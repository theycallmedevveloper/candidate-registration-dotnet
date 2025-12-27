/* ################--------- Date Picker ---------################ */
$(document).ready(function () {
    $('#dob').datepicker({
        format: 'dd/mm/yyyy',
        startDate: '01/01/1900',
        endDate: new Date(),
        autoclose: true,
        todayHighlight: true
    });

    /* ################--------- Dropdown Loader ---------################ */
    loadPrefixes();
    loadGenders();
    loadMaritalStatuses();
    loadCandidates();

});

let entries = [];
let editingCandidateId = null;

/* ################--------- API Loaders ---------################ 007 */
function loadPrefixes() {
    $.ajax({
        url: "https://localhost:7288/api/prefix",
        method: "GET",
        success: function (data) {
            const ddl = $("#PrefixId");
            ddl.empty();
            ddl.append(`<option value="">Marital Status</option>`);
            data.forEach(p => {
                ddl.append(`<option value="${p.prefix_Id}">${p.prefix_Name}</option>`);
            });
        },
        error: function () {
            console.error("Prefix API failed");
        }
    });
}

function loadMaritalStatuses() {
    $.ajax({
        url: "https://localhost:7288/api/maritalstatus",
        method: "GET",
        success: function (data) {
            const container = $("#maritalStatusContainer");
            container.empty();

            data.forEach((ms, index) => {
                const id = `marital_${ms.maritalStatus_Id}`;

                container.append(`
                    <div class="col-md-6 radio-col">
                        <div class="form-check mb-2">
                            <input class="form-check-input"
                                   type="radio"
                                   name="maritalStatusId"
                                   id="${id}"
                                   value="${ms.maritalStatus_Id}"
                                   required>
                            <label class="form-check-label" for="${id}">
                                ${ms.maritalStatus_Name}
                            </label>
                        </div>
                    </div>
                `);
            });
        },
        error: function () {
            console.error("Marital Status API failed");
        }
    });
}

function loadGenders() {
    $.ajax({
        url: "https://localhost:7288/api/gender",
        method: "GET",
        success: function (data) {
            const container = $("#genderContainer");
            container.empty();

            data.forEach(g => {
                const id = `gender_${g.gender_Id}`;

                container.append(`
                    <div class="col-md-6 radio-col">
                        <div class="form-check mb-2">
                            <input class="form-check-input"
                                   type="radio"
                                   name="genderId"
                                   id="${id}"
                                   value="${g.gender_Id}"
                                   required>
                            <label class="form-check-label" for="${id}">
                                ${g.gender_Name}
                            </label>
                        </div>
                    </div>
                `);
            });
        },
        error: function () {
            console.error("Gender API failed");
        }
    });
}

/* ################--------- Existing Candidate in API ---------################ */
function loadCandidates() {
    $.ajax({
        url: "https://localhost:7288/api/candidate",
        method: "GET",
        success: function (data) {
            entries = data;
            renderTable();
        },
        error: function (err) {
            console.error("Failed to load candidates:", err);
        }
    });
}

/* ################--------- Form Submit Handler ---------################ */
document.getElementById('candidateForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const formData = new FormData(this);
    const candidateData = {
        Candidate_Id: editingCandidateId || 0,
        Prefix_Id: parseInt(formData.get('Prefix_Name')),
        FirstName: formData.get('firstName'),
        MiddleName: formData.get('middleName') || '',
        LastName: formData.get('lastName'),
        Gender_Id: parseInt(formData.get('genderId')),
        Dob: formData.get('dob'),
        MaritalStatus_Id: parseInt(formData.get('maritalStatusId')),
        Candidate_Email: formData.get('email'),
        Candidate_Num: formData.get('phone')
    };


    /* ################--------- Post API ---------################ */
    $.ajax({
        url: "https://localhost:7288/api/Candidate/SaveCandidate1",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(candidateData),
        success: function (response) {
            alert(response.message || "Saved successfully!");

            loadCandidates();

            editingCandidateId = null;
            updateSubmitButtonText();
            document.getElementById('candidateForm').reset();
        },
        error: function (err) {
            console.error("API failed:", err);
            alert("Failed to submit. Check console for details.");
        }
    });
});


/* ################--------- Clear Form ---------################ */
document.getElementById('clearBtn').addEventListener('click', function () {
    editingCandidateId = null;
    updateSubmitButtonText();
    document.getElementById('candidateForm').reset();
});


/* ################--------- Table with all entries ---------################ */
function renderTable() {
    const tbody = document.getElementById('entriesBody');

    if (entries.length === 0) {
        tbody.innerHTML = '<tr><td colspan="8" class="text-center text-muted">No entries yet. Submit the form above to see data here!</td></tr>';
        return;
    }


    tbody.innerHTML = entries.map((entry, index) => {
        const fullName = `${entry.prefix?.prefix_Name || ''} ${entry.firstName} ${entry.middleName || ''} ${entry.lastName}`.replace(/\s+/g, ' ').trim();
        const candidateId = entry.candidate_Id || entry.Candidate_Id || entry.id || index;

        return `
                        <tr>
                        
                            <td>${entry.candidate_Id}</td>
                            <td>${fullName}</td>
                            <td>${entry.gender?.gender_Name || entry.gender_Id}</td>
                            <td>${entry.dob || 'N/A'}</td>
                            <td>${entry.maritalStatus?.maritalStatus_Name || entry.maritalStatus_Id}</td>
                            <td>${entry.email || 'N/A'}</td>
                            <td>${entry.number || entry.candidate_Num || 'N/A'}</td>
                            <td>
                                <button class="btn btn-sm btn-warning me-2" onclick="editEntry(${entry.candidate_Id})">
                                    Update
                                </button>
                                <span class="delete-btn" onclick="deleteEntry(${entry.candidate_Id})"   title="Delete entry">
                                    ✖
                                </span>
                            </td>

                        </tr>
                    `;
    }).join('');
}

function toMMDDYYYY(dateStr) {
    if (!dateStr) return "";

    // handles "2025-12-26" and "2025-12-26T00:00:00"
    const date = new Date(dateStr);
    const dd = String(date.getDate()).padStart(2, '0');
    const mm = String(date.getMonth() + 1).padStart(2, '0');

    const yyyy = date.getFullYear();

    return `${dd}/${mm}/${yyyy}`;
}


/* ################--------- Edit Entry ---------################ */
function editEntry(id) {
    $.ajax({
        url: `https://localhost:7288/api/candidate/${id}`,
        type: "GET",
        success: function (data) {

            editingCandidateId = data.candidate_Id;

            $("#PrefixId").val(data.prefix_Id || data.prefix?.prefix_Id);

            $("#fName").val(data.candidate_FirstName || data.firstName);
            $("#mName").val(data.candidate_MiddleName || data.middleName);
            $("#lName").val(data.candidate_LastName || data.lastName);
            // $("#dob").val(data.candidate_Dob || data.dob);
            $("#dob").val(toMMDDYYYY(data.candidate_Dob || data.dob));

            $("#candidateEmail").val(data.candidate_Email || data.email);
            $("#candidatePhone").val(data.candidate_Num || data.number);
            $(`input[name="genderId"][value="${data.gender_Id || data.gender?.gender_Id}"]`)
                .prop("checked", true);
            $(`input[name="maritalStatusId"][value="${data.maritalStatus_Id || data.maritalStatus?.maritalStatus_Id}"]`)
                .prop("checked", true);

            window.scrollTo({ top: 0, behavior: "smooth" });

            editingCandidateId = data.candidate_Id;
            updateSubmitButtonText();
        },
        error: function () {
            alert("Failed to load candidate data");

        }

    });
}

/* ################--------- Delete Entry ---------################ */
function deleteEntry(id) {
    console.log("Deleting candidate ID:", id);

    if (!id) {
        alert("Invalid candidate ID");
        return;
    }

    if (confirm('Are you sure you want to delete this entry?')) {
        $.ajax({
            url: `https://localhost:7288/api/candidate/${id}`,
            type: "DELETE",
            success: function () {
                alert('Entry deleted successfully!');
                loadCandidates();
            },
            error: function (err) {
                console.error("Delete failed:", err.responseJSON || err);
                alert(err.responseJSON?.message || "Delete failed");
            }
        });
    }
}

/* ################--------- Update Submit Button ---------################ */
function updateSubmitButtonText() {
    if (editingCandidateId) {
        $("button[type='submit']").text("Update Candidate");
    } else {
        $("button[type='submit']").text("Submit");
    }
}

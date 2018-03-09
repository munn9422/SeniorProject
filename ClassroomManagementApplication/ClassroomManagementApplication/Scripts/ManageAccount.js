function editRole() {
    var form = document.getElementByID('editRoleForm');
    if (form.style.display === "none") {
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
}

function chooseRole() {
    var form = getElementById('editRoleForm');
    form.style.display = 'block';
}
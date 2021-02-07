function validateForm() {
    var x = document.forms["myForm"]["city"].value;
    if (x === "") {
        alert("Field is empty! Please insert some City");
        return false;
    }
}
function checkPasswordStrength() {
    const password = document.getElementById("password").value;

    // Kontrol Edilecek Kurallar
    const lengthCheck = password.length >= 6;
    const lowercaseCheck = /[a-z]/.test(password);
    const uppercaseCheck = /[A-Z]/.test(password);
    const specialCharCheck = /[\W_]/.test(password);
    const numberCheck = /[0-9]/.test(password);

    // Checklist Güncelleme
    updateChecklist("lengthCheck", lengthCheck);
    updateChecklist("lowercaseCheck", lowercaseCheck);
    updateChecklist("uppercaseCheck", uppercaseCheck);
    updateChecklist("specialCharCheck", specialCharCheck);
    updateChecklist("numberCheck", numberCheck);

    // Şifre Gücü Belirleme
    const strengthText = document.getElementById("strengthText");
    const strengthBar = document.getElementById("strengthBar");
    let strength = 0;
    if (lengthCheck) strength++;
    if (lowercaseCheck) strength++;
    if (uppercaseCheck) strength++;
    if (specialCharCheck) strength++;
    if (numberCheck) strength++;

    if (strength <= 2) {
        strengthText.textContent = "Güçsüz";
        strengthText.className = "password-strength-text text-weak";
        strengthBar.style.backgroundColor = "red";
    } else if (strength === 3 || strength === 4) {
        strengthText.textContent = "Orta";
        strengthText.className = "password-strength-text text-medium";
        strengthBar.style.backgroundColor = "orange";
    } else if (strength === 5) {
        strengthText.textContent = "Güçlü";
        strengthText.className = "password-strength-text text-strong";
        strengthBar.style.backgroundColor = "green";
    }
}

function updateChecklist(id, isValid) {
    const element = document.getElementById(id);
    if (isValid) {
        element.classList.add("valid");
    } else {
        element.classList.remove("valid");
    }
}
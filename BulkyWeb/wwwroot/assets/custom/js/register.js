const formElement = document.getElementById('formElement');
const username = document.getElementById('username');
const lastname = document.getElementById('lastname');
const email = document.getElementById('email');
const email2 = document.getElementById('email2');
const phone = document.getElementById('phone');
const address = document.getElementById('address');
const country = document.getElementById('country');
const city = document.getElementById('city');
const password = document.getElementById('password');
const password2 = document.getElementById('password2');
// const submitButton = document.getElementById('submit-btn');

formElement.addEventListener('input', e => {
    checkInputs();
});

formElement.addEventListener('submit', e => {
    e.preventDefault();
    checkInputs();
});
function checkInputs() {
    const usernameValue = username.value.trim();
    const lastnameValue = lastname.value.trim();
    const emailValue = email.value.trim();
    const phoneValue = phone.value.trim();
    const addressValue = address.value.trim();
    const countryValue = country.value.trim();
    const cityValue = city.value.trim();
    const passwordValue = password.value.trim();
    const password2Value = password2.value.trim();
    const email2Value = email2.value.trim();
    // let formValid = true;

    // Username validation
    if (usernameValue === '') {
        // setErrorFor(username, 'Username cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(username);
    }

    // Last name validation
    if (lastnameValue === '') {
        // setErrorFor(lastname, 'Last name cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(lastname);
    }

    // Email validation
    if (emailValue === '') {
        // setErrorFor(email, 'Email cannot be blank');
        // formValid = false;
    } else if (!isEmail(emailValue)) {
        setErrorFor(email, 'Not a valid email');
        // formValid = false;
    } else {
        setSuccessFor(email);
    }

    // Confirm Email validation
    if (email2Value === '') {
        // setErrorFor(email2, 'Confirm email cannot be blank');
        // formValid = false;
    } else if (emailValue !== email2Value) {
        setErrorFor(email2, 'Emails do not match');
        // formValid = false;
    } else {
        setSuccessFor(email2);
    }

    // Phone validation
    // if (phoneValue === '') {
    //     // setErrorFor(phone, 'Phone number cannot be blank');
        formValid = false;
    // } else if (!isPhone(phoneValue)) {
    //     setErrorFor(phone, 'Phone number is not valid');
        formValid = false;
    // } else {
    //     setSuccessFor(phone);
    // }

    // Address validation
    if (addressValue === '') {
        // setErrorFor(address, 'Address cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(address);
    }

    // Country validation
    if (countryValue === '') {
        // setErrorFor(country, 'Country cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(country);
    }

    // City validation
    if (cityValue === '') {
        // setErrorFor(city, 'City cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(city);
    }

    // Password validation
    if (passwordValue === '') {
        // setErrorFor(password, 'Password cannot be blank');
        // formValid = false;
    } else {
        setSuccessFor(password);
    }

    // Password2 validation (match check)
    if (password2Value === '') {
        // setErrorFor(password2, 'Confirm Password cannot be blank');
        // formValid = false;
    } else if (passwordValue !== password2Value) {
        setErrorFor(password2, 'Passwords do not match');
        // formValid = false;
    } else {
        setSuccessFor(password2);
    }

    // Enable/Disable the submit button based on form validity
    // if (formValid) {
    //     submitButton.disabled = false; // Enable button
    // } else {
    //     submitButton.disabled = true; // Disable button
    // }
}





// Real-time email match check as the user types
// email.addEventListener('input', validateEmailMatch);
// email2.addEventListener('input', validateEmailMatch);

// function validateEmailMatch() {
//     const emailValue = email.value.trim();
//     const email2Value = email2.value.trim();

//     if (email2Value === '') {
//         setErrorFor(email2, 'Confirm Email cannot be blank');
//     } else if (emailValue !== email2Value) {
//         setErrorFor(email2, 'Emails do not match');
//     } else {
//         setSuccessFor(email2);
//     }
// }


// Real-time password match check as the user types
password.addEventListener('input', validatePasswordMatch);
password2.addEventListener('input', validatePasswordMatch);

function validatePasswordMatch() {
    const passwordValue = password.value.trim();
    const password2Value = password2.value.trim();
    if (password2Value === '') {
        setErrorFor(password2, 'Confirm Password cannot be blank');
    } else if (passwordValue !== password2Value) {
        setErrorFor(password2, 'Passwords do not match');
    } else {
        setSuccessFor(password2);
    }
}


// Real-time phone validation (prevents non-numeric input)
// phone.addEventListener('keydown', function (e) {
//     // Allow only numeric values (and a few control keys like backspace)
//     if (e.key !== 'Backspace' && e.key !== 'Tab' && e.key !== 'ArrowLeft' && e.key !== 'ArrowRight' && !/^\d$/.test(e.key)) {
//         e.preventDefault(); // Prevent non-numeric input
//     }
// });

function setErrorFor(input, message) {
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    formControl.className = 'form-control error';
    small.innerText = message;
}

function setSuccessFor(input) {
    const formControl = input.parentElement;
    formControl.className = 'form-control success';
}

function isEmail(email) {
    return /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email);
}

function isPhone(phone) {
    return /^\d{4}[-.\s]?\d{3}[-.\s]?\d{4}$/.test(phone); // Matches phone formats like 123-456-7890
}


////////////////////the countries///////////////////////////////////////////

document.getElementById("country").addEventListener("change", function () {
    var country = this.value;

    var citySelect = document.getElementById("city");
    citySelect.innerHTML = "";

    var cities = [];

    if (country === "السعودية") {
        cities = [
            "الرياض", "جدة", "الدمام", "مكة", "المدينة المنورة", "الطائف", "بريدة", "أبها",
            "خميس مشيط", "القصيم", "حائل", "الجبيل", "تبوك", "الاحساء", "نجران", "جازان",
            "الحدود الشمالية", "الباحة", "الجوف", "الظهران", "القطيف", "المدينة", "رفحاء",
            "الخرج", "النعيرية", "الرياض", "الزلفي", "البدائع", "المجمعة", "المخواة", "الدلم"
        ];
    } else if (country === "الإمارات") {
        cities = ["دبي", "أبوظبي", "الشارقة", "العين", "رأس الخيمة", "الفجيرة", "عجمان", "أم القيوين"];
    } else if (country === "مصر") {
        cities = [
            "القاهرة", "الإسكندرية", "الجيزة", "شرم الشيخ", "الأقصر", "أسوان", "بورسعيد", "دمنهور",
            "طنطا", "المنصورة", "بنها", "سوهاج", "قنا", "كفر الشيخ", "الزقازيق", "بني سويف",
            "الفيوم", "المنيا", "أسيوط", "دمياط", "السويس", "العريش", "الواحات", "مطروح",
            "الشرقية", "الإسماعيلية", "المحلة الكبرى", "العين السخنة", "وادي النطرون",
            "الجيزة", "الفيوم", "المنيا", "أسيوط", "البحر الأحمر", "الدقهلية", "الجيزة"
        ];
    } else if (country === "الولايات المتحدة الأمريكية") {
        cities = ["كاليفورنيا", "تكساس", "فلوريدا", "نيويورك", "بنسلفانيا", "إلينوي",
            "أوهايو", "جورجيا", "كارولينا الشمالية", "ميشيغان", "نيوجيرسي", "فرجينيا",
            "واشنطن", "أريزونا", "ماساشوستس", "تينيسي", "إنديانا", "ميزوري", "مريلاند", "ويسكونسن"];
    } else if (country === "كندا") {
        cities = ["أونتاريو", "كيبيك", "نوفا سكوشا", "نيو برونزويك", "مانيتوبا", "كولومبيا البريطانية",
            "جزيرة الأمير إدوارد", "ساسكاتشوان", "ألبرتا", "نيوفاوندلاند ولابرادور", "الأقاليم الشمالية الغربية",
            "يوكّون", "نونافوت"];
    } else if (country === "المملكة المتحدة") {
        cities = ["لندن", "مانشستر", "برمنغهام", "ليفربول", "إدنبرة", "غلاسكو", "ليستر", "بلفاست", "شيفيلد", "نيوكاسل"];
    }
    else if (country === "ألمانيا") {
        cities = ["برلين", "ميونيخ", "فرانكفورت", "هامبورغ", "كولونيا", "دوسلدورف", "شتوتغارت", "لايبزيغ", "دويسبورغ", "بون"];
    }
    else if (country === "فرنسا") {
        cities = ["باريس", "مارسيليا", "ليون", "بوردو", "تولوز", "نيس", "نانت", "ستراسبورغ", "ليل", "مونبلييه"];
    }
    else if (country === "إيطاليا") {
        cities = ["روما", "ميلانو", "نابولي", "تورينو", "فلورنسا", "بولونيا", "فينيسيا", "جنوة", "باليرمو", "باري"];
    }
    else if (country === "أستراليا") {
        cities = ["سيدني", "ملبورن", "بريسبان", "أديلايد", "بيرث", "هوبارت", "كانبيرا", "داروين"];
    }
    else if (country === "البرازيل") {
        cities = ["ريو دي جانيرو", "ساو باولو", "برازيليا", "سلفادور", "ريسيفي", "بورتو أليغري", "فورتاليزا", "ماناوس"];
    }
    else if (country === "الهند") {
        cities = ["دلهي", "مومباي", "بنغالور", "كلكتا", "حيدر آباد", "تشيناي", "أحمد آباد", "بوني",
            "جايبور", "كوتشي", "لوديانا", "لكهنؤ", "فاراناسي", "شانديغار", "بوبال"];
    }
    else if (country === "الجزائر") {
        cities = ["الجزائر العاصمة", "وهران", "قسنطينة", "عنابة", "مستغانم", "سطيف", "باتنة",
            "تبسة", "بومرداس", "بشار", "معسكر", "تيزي وزو", "المدية", "بليدة", "خنشلة",
            "أم البواقي", "ميلة"];
    }
    else if (country === "روسيا") {
        cities = ["موسكو", "سانت بطرسبرغ", "نوفوسيبيرسك", "يكاترينبورغ", "نيجني نوفغورود",
            "قازان", "فولغوغراد", "أومسك", "تشيليابينسك", "روستوف على نهر الدون",
            "سامارا", "كراسنويارسك", "تولا", "سيربوخوف", "إيركوتسك", "فلاديفوستوك"];
    }
    else if (country === "الصين") {
        cities = ["بكين", "شنغهاي", "غوانغتشو", "شينزين", "تشنغدو", "هونغ كونغ", "هاربين",
            "تشونغتشينغ", "ووهان", "تيانجين", "سوتشو", "تشينغداو", "نانجينغ", "نينغبو"];
    }
    else if (country === "اليابان") {
        cities = ["طوكيو", "أوساكا", "كيوتو", "هوكايدو", "ناغويا", "هيروشيما", "فوكوكا",
            "كوبه", "سايتاما", "شينجيكو", "نارا", "كاغوشيما", "سابورو", "يوكوهاما", "أكيتا"];
    }
    else if (country === "المكسيك") {
        cities = ["مكسيكو سيتي", "جوادالاخارا", "مونتيري", "كانكون", "ميريدا", "بويبلا",
            "تيخوانا", "أكابولكو", "أواكساكا", "ليون", "سان لويس بوتوسي", "كويريتارو",
            "سالتيو", "فيراكروز", "تشيهواهوا"];
    }
    else if (country === "جنوب أفريقيا") {
        cities = ["كيب تاون", "جوهانسبرغ", "دوربان", "بريتوريا", "بورت إليزابيث", "بلومفونتين",
            "نيلسبروت", "كيمبرلي", "بوشبوش", "ميدراند", "ريتشمونت", "كوازولو ناتال", "ستيليبي", "جوهانسبرغ الشمالية"];
    }
    else if (country === "إسبانيا") {
        cities = ["مدريد", "برشلونة", "فالنسيا", "إشبيلية", "غرناطة", "مالقة", "سرقسطة", "بلباو",
            "مايوركا", "فيغو", "ساباديل", "أليكانتي", "إشبيلية", "بلد الوليد", "تينيريفي"];
    }
    else if (country === "تركيا") {
        cities = ["إسطنبول", "أنقرة", "إزمير", "أنطاليا", "بورصا", "أدانا", "غازي عنتاب", "قونيا",
            "أزمير", "مارماريس", "طرابزون", "كابادوكيا", "سامسون", "موغلا"];
    }
    else if (country === "اليونان") {
        cities = ["أثينا", "سالونيكا", "باتراس", "إيراكليون", "هيراكليون", "لاريسا", "رودس",
            "كريت", "كورفو", "ميتيورا", "كالاماتا", "زاكينثوس"];
    }
    else if (country === "لبنان") {
        cities = ["بيروت", "طرابلس", "صيدا", "زحلة", "جونية", "جبيل", "صور", "بعلبك", "الدامور", "عاليه"];
    }
    else if (country === "الأردن") {
        cities = ["عمان", "إربد", "الزرقاء", "البتراء", "العقبة", "مأدبا", "الكرك", "السلط", "عجلون", "معان"];
    }
    else if (country === "فلسطين") {
        cities = ["القدس", "رام الله", "غزة", "بيت لحم", "نابلس", "الخليل", "جنين", "طولكرم", "طوباس", "قلقيلية"];
    }
    else if (country === "العراق") {
        cities = ["بغداد", "البصرة", "أربيل", "الموصل", "كربلاء", "النجف", "بابل", "السليمانية", "ديالى", "دهوك"];
    }
    else if (country === "سوريا") {
        cities = ["دمشق", "حلب", "اللاذقية", "حمص", "طرطوس", "درعا", "إدلب", "دير الزور", "الرقة", "القنيطرة",
            "السويداء", "الميادين", "الحسكة", "المعضمية", "دوما", "جسر الشغور", "القامشلي", "الباب",
            "كفرنبل", "الزبداني"];
    }
    else if (country === "الكويت") {
        cities = ["مدينة الكويت", "حولي", "الفروانية", "الجهراء", "القبلة", "الأحمدي", "السالمية", "المنطقة الصناعية", "الماجدية", "الهدية"];
    }
    else if (country === "عمان") {
        cities = ["مسقط", "صلالة", "نزوى", "صحار", "البريمي", "الخابورة", "الرستاق", "بهلاء", "مطرح", "إبراء"];
    }
    else if (country === "اليمن") {
        cities = ["صنعاء", "عدن", "تعز", "المكلا", "المأرب", "الحديدة", "البيضاء", "إب", "الضالع", "شبوة"];
    }
    else if (country === "تونس") {
        cities = ["تونس", "سوسة", "صفاقس", "المهدية", "قابس", "المنستير", "نابل", "قيروان", "بنزرت", "جندوبة"];
    }
    else if (country === "المغرب") {
        cities = ["الرباط", "الدار البيضاء", "مراكش", "فاس", "طنجة", "أغادير", "مكناس", "العيون", "وجدة", "تطوان"];
    }
    else if (country === "ليبيا") {
        cities = ["طرابلس", "بنغازي", "مصراتة", "البيضاء", "الزاوية", "سبها", "طبرق", "درنة", "زوارة", "الخمس"];
    }
    else if (country === "موريتانيا") {
        cities = [
            "نواكشوط", "نواذيبو", "أطار", "وادان", "شنقيط",
            "ألاك", "بوكي", "مقطع لحجار", "زويرات", "فديرك",
            "تجكجة", "المجرية", "كيفه", "بومديد", "العيون",
            "بوغي", "امبود", "عدي بكرو", "جكني", "لعيون",
            "كوبني", "الركيز", "سيدي ولد يوسف"
        ];
    }
    else if (country === "البحرين") {
        cities = [
            "المنامة", "المحرق", "الحد", "السترة", "الرفاع",
            "أم الحصم", "الزلاق", "المالكية", "الهملة", "النبيه صالح",
            "البديع", "عالي", "الشاخورة", "الجفير", "جزر حوار"
        ];
    }
    else if (country === "السودان") {
        cities = [
            "الخرطوم", "أم درمان", "الجزيرة", "الفاشر", "بورتسودان",
            "كوستي", "نيالا", "مدني", "الأبيض", "الأزهر",
            "الدمازين", "الخرطوم بحري", "الجنينة", "الضعين", "سنار",
            "كادقلي", "الدويم", "كريمة", "الفاو", "بربر"
        ];
    }
    else if (country === "جزر القمر") {
        cities = [
            "موراوني", "فومبوني", "أوردوغو", "نيزواني", "موهلي",
            "أنجوان", "تولهي", "الجزيرة الكبرى", "أمبوين"
        ];
    }


    cities.sort();

    cities.forEach(function (city) {
        var option = document.createElement("option");
        option.value = city;
        option.textContent = city;
        citySelect.appendChild(option);
    });
});

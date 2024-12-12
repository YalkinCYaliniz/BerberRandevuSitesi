document.getElementById('uploadButton').addEventListener('click', async function () {
    const photoInput = document.getElementById('uploadPhoto');
    if (!photoInput.files.length) {
        alert("Lütfen bir fotoğraf seçin!");
        return;
    }

    const formData = new FormData();
    formData.append("file", photoInput.files[0]);

    // Seçilen saç modelini al
    const selectedHairStyle = document.getElementById('hairStyleSelect').value;

    try {
        // API'ye fotoğrafı ve seçilen modeli gönderme
        const url = 'https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro';
        const options = {
            method: 'POST',
            headers: {
                'x-rapidapi-key': 'b833913418msh34293360c392990p168517jsnfa01e09de061',
                'x-rapidapi-host': 'hairstyle-changer-pro.p.rapidapi.com',
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: new URLSearchParams({
                hair_style: selectedHairStyle, // Kullanıcının seçtiği saç modeli
                task_type: 'hairstyle' // Bu parametre yine 'hairstyle' olarak kalacak
            })
        };

        const response = await fetch(url, options);
        const result = await response.json(); // Yanıt JSON formatında döner

        console.log(result); // Yanıtı konsolda kontrol et

        if (result && result.image_url) {
            const outputImage = document.createElement("img");
            outputImage.src = result.image_url;
            outputImage.alt = "Yeni Saç Modeliniz";
            document.getElementById("result").innerHTML = ''; // Önceki sonucu temizle
            document.getElementById("result").appendChild(outputImage); // Sonucu göster
        } else {
            alert("Saç modeli denemesi başarısız!");
        }
    } catch (error) {
        console.error("Hata:", error);
        alert("Bir hata oluştu. Lütfen tekrar deneyin.");
    }
});

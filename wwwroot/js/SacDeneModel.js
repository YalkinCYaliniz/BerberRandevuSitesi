// API Bağlantı Bilgileri
const apiUrl = "https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro";
const apiKey = "b833913418msh34293360c392990p168517jsnfa01e09de061";

// Saç Modeli Uygulama Fonksiyonu
async function getStyledImage(hairStyle, imageFile) {
    try {
        // FormData oluştur
        const formData = new FormData();
        formData.append("file", imageFile, "image.jpg");
        formData.append("hair_style", hairStyle);
        formData.append("task_type", "hairstyle");

        // API çağrısı
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "x-rapidapi-key": apiKey,
                "x-rapidapi-host": "hairstyle-changer-pro.p.rapidapi.com",
            },
            body: formData,
        });

        // Yanıtı kontrol et
        if (response.ok) {
            const jsonResponse = await response.json();
            return jsonResponse; // Başarılı yanıtı döndür
        } else {
            console.error("API Hatası:", response.status, response.statusText);
            return null; // Hata durumunda
        }
    } catch (error) {
        console.error("Bir hata oluştu:", error);
        return null;
    }
}

// Kullanıcıdan fotoğraf alıp API çağrısı yapan örnek kullanım
document.getElementById("uploadButton").addEventListener("click", async () => {
    const fileInput = document.getElementById("uploadPhoto");
    const selectedHairStyle = document.getElementById("hairStyleSelect").value;

    if (!fileInput.files.length || !selectedHairStyle) {
        alert("Lütfen bir fotoğraf seçin ve saç modeli belirleyin!");
        return;
    }

    const imageFile = fileInput.files[0];
    const result = await getStyledImage(selectedHairStyle, imageFile);

    if (result && result.image_url) {
        // Sonucu ekrana göster
        const outputImage = document.getElementById("outputImage");
        outputImage.src = result.image_url;
        outputImage.alt = "Yeni Saç Modeliniz";
    } else {
        alert("Saç modeli uygulama başarısız oldu!");
    }
});

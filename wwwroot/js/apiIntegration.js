document.getElementById("uploadButton").addEventListener("click", async () => {
  const photoInput = document.getElementById("photo");
  const hairType = document.getElementById("hairType").value;

  if (!photoInput.files[0]) {
    alert("Lütfen bir fotoğraf seçin.");
    return;
  }

  const formData = new FormData();
  formData.append("image_target", photoInput.files[0]); // Fotoğraf dosyasını ekle
  formData.append("hair_type", hairType); // Saç tipi ID'sini ekle

  try {
    const response = await fetch(
      "https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle",
      {
        method: "POST",
        headers: {
          "x-rapidapi-key":
            "e8ead7a5d1mshe7bcacc07767246p1d1832jsn1a5c0323fdb",
          "x-rapidapi-host": "hairstyle-changer.p.rapidapi.com",
        },
        body: formData,
      }
    );

    if (!response.ok) {
      const errorText = await response.text();
      console.error(`API Hatası: ${errorText}`);
      alert(`API Hatası: ${response.status} - ${errorText}`);
      return;
    }

    const jsonResponse = await response.json();
    console.log("API Yanıtı:", jsonResponse);

    if (jsonResponse.data && jsonResponse.data.image) {
      const base64Image = jsonResponse.data.image;

      // İşlenmiş görüntüyü göster
      document.getElementById("result").innerHTML = `
                <h3>Sonuç:</h3>
                <img src="data:image/jpeg;base64,${base64Image}" alt="Sonuç" style="max-width: 100%;" />
            `;
    } else {
      alert("Yanıt içinde görüntü bulunamadı.");
    }
  } catch (error) {
    console.error("Hata:", error);
    alert("Fotoğraf işlenirken bir hata oluştu.");
  }
});

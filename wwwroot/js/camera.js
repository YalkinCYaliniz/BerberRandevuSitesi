// Kamera ile çekim için kod
const video = document.getElementById("cameraStream");
const captureButton = document.getElementById("captureButton");

navigator.mediaDevices.getUserMedia({ video: true })
    .then(stream => {
        video.srcObject = stream;
    })
    .catch(error => {
        console.error("Kamera erişimi başarısız:", error);
        alert("Kameraya erişim izni verin!");
    });

captureButton.addEventListener('click', () => {
    const canvas = document.createElement("canvas");
    const context = canvas.getContext("2d");

    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    context.drawImage(video, 0, 0, canvas.width, canvas.height);

    // Çekilen görüntüyü RapidAPI'ye gönderebilirsiniz
    canvas.toBlob(blob => {
        const formData = new FormData();
        formData.append("file", blob);

        // RapidAPI çağrısı buradan yapılabilir
    });
});

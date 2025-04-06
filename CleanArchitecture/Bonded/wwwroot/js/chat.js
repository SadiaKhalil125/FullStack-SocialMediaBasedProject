const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();
connection.start().then(() => {
    const senderId = document.getElementById("senderId").value;
    const receiverId = document.getElementById("receiverId").value;

   
    connection.invoke("JoinChat", senderId, receiverId)
        .catch(err => console.error("JoinChat Error: ", err.toString()));
}).catch(err => console.error("Connection Error: ", err.toString()));


connection.on("ReceiveMessage", (senderId, message, profilePicture) => {
    const currentUserId = document.getElementById("senderId").value;
    let messageContainer = document.createElement("div");

    if (senderId === currentUserId) {
        messageContainer.classList.add("message", "sent");
    } else {
        messageContainer.classList.add("message", "received");
    }
   
    let img = document.createElement("img");
    img.src = profilePicture;
    img.alt = "Profile Picture";
    
    let textDiv = document.createElement("div");
    textDiv.classList.add("text");
    textDiv.innerHTML = `${message} <br><small class="text-muted">${new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</small>`;
  
    if (senderId === currentUserId) {
        let deleteBtn = document.createElement("button");
        deleteBtn.classList.add("delete-btn");
        deleteBtn.innerHTML = `<i class="bi bi-x-circle"></i>`;
        deleteBtn.onclick = function () {
            deleteMessage(messageContainer.dataset.id, this);
        };

        // Append elements for sent messages (delete button on the extreme left)
        messageContainer.appendChild(deleteBtn);
        messageContainer.appendChild(img);
        messageContainer.appendChild(textDiv);
    } else {
        // Append elements for received messages
        messageContainer.appendChild(img);
        messageContainer.appendChild(textDiv);
    }

    document.getElementById("chat-box").appendChild(messageContainer);
    document.getElementById("chat-box").scrollTop = document.getElementById("chat-box").scrollHeight;
});


function sendMessage() {
    const senderId = document.getElementById("senderId").value;
    const receiverId = document.getElementById("receiverId").value;
    const message = document.getElementById("messageInput").value;

    if (!senderId || !receiverId || !message) {
        alert("Please enter sender ID, receiver ID, and a message.");
        return;
    }

    connection.invoke("SendMessage", senderId, receiverId, message)
        .catch(err => console.error("SendMessage Error: ", err.toString()));

    document.getElementById("messageInput").value = "";
}


function deleteMessage(messageId, button) {
    $.ajax({
        url: '/Chat/DeleteMessage/',
        type: "POST",
        data: { msgId: messageId }, // Sending messageId in case the server needs it
        success: function (response) {
            if (response.success) { // Assuming the server responds with { success: true }
                const messageElement = $(button).closest(".message");
                if (messageElement.length) {
                    messageElement.remove();
                }
            } else {
                alert(response.message || "Failed to delete message.");
            }
        },
        error: function (xhr, status, error) {
            console.error("DeleteMessage Error:", error);
            alert("An error occurred while deleting the message.");
        }
    });
}






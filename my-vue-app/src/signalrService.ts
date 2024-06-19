import * as signalR from "@microsoft/signalr";

let connection: signalR.HubConnection;

export function startConnection() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://your-api-url/hub") // Thay thế bằng URL API của bạn
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection
    .start()
    .then(() => console.log("SignalR connection started"))
    .catch((err) =>
      console.log("Error while starting SignalR connection: " + err)
    );

  connection.on("CartUpdated", () => {
    console.log("Cart updated notification received.");
    // Phát ra một sự kiện tùy chỉnh để thông báo cho các component Vue
    document.dispatchEvent(new CustomEvent("cartUpdated"));
  });
}

export function getConnection() {
  return connection;
}

using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class CartHub : Hub
    {
        public async Task NotifyCartUpdated(string userName)
        {
            await Clients.User(userName).SendAsync("CartUpdated");
        }
    }
}

// Gọi NotifyCartUpdated mỗi khi có thay đổi trong giỏ hàng. Ví dụ:
public class CartService
{
    private readonly IHubContext<CartHub> _hubContext;

    public CartService(IHubContext<CartHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddToCartAsync(string userName, string productId)
    {
        // Logic thêm sản phẩm vào giỏ hàng...

        // Thông báo cập nhật giỏ hàng cho user
        await _hubContext.Clients.User(userName).SendAsync("CartUpdated");
    }
}

<template>
    <div class="order-item">
        <div class="order-info">
            <div>Mã đơn hàng: {{ order.OrderID }}</div>
            <div>Ngày đặt: {{ order.OrderDate }}</div>
            <div>Trạng thái: {{ order.Status }}</div>
        </div>
        <div class="product-info">
            <div class="product">
                <img :src="order.Image" alt="Product Image" />
                <div class="details">
                    <div class="name">{{ order.ProductName }}</div>
                    <div class="quantity">Số lượng: {{ order.Quantity }}</div>
                    <div class="price">Giá: {{ order.Price }}</div>
                </div>
            </div>
            <div v-if="order.Status === 'Chờ xác nhận'" class="actions">
                <button @click="removeOrder(order.OrderID)">Hủy đơn hàng</button>
            </div>
            <div v-else-if="order.Status === 'Chờ lấy hàng'" class="actions">
                <button @click="completeOrder(order.OrderID)">Hoàn thành</button>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'; // Import axios library

export default {
    props: {
        order: {
            type: Object,
            required: true
        }
    },
    methods: {
        async removeOrder(orderID) {
            try {
                const userName = this.userName;
                await axios.delete(`http://localhost:5022/api/Orders/${userName}/${orderID}`);
                console.log('Đã hủy đơn hàng có ID:', orderID);
                // Sau khi thành công, bạn có thể cập nhật lại danh sách đơn hàng hoặc thực hiện các thao tác khác cần thiết.
            } catch (error) {
                console.error('Lỗi khi hủy đơn hàng:', error);
            }
        },
        async completeOrder(orderID) {
            try {
                const userName = this.order.UserName; // Assuming UserName is part of the order object
                const response = await axios.put(`http://localhost:5022/api/Orders/${userName}/${orderID}`, '"Chờ lấy hàng"', {
                    headers: {
                        'accept': '*/*',
                        'Content-Type': 'application/json-patch+json'
                    }
                });
                console.log('Order confirmed:', response.data);
                // Optionally, update the local state or notify the parent component about the update
            } catch (error) {
                console.error('Error confirming order:', error);
            }
        }
       },
    computed: {
        userName() {
            // Trả về userName từ state hoặc localStorage
            return this.$store.state.userName || localStorage.getItem('userName');
        },
    },
}
</script>

<style scoped>
.order-item {
    display: flex;
    justify-content: space-between;
    border: 1px solid #ccc;
    margin-bottom: 10px;
    padding: 10px;
}

.order-info,
.product-info {
    flex: 1;
}

.product {
    display: flex;
    align-items: center;
}

.product img {
    width: 80px;
    height: 80px;
    margin-right: 10px;
}

.actions button {
    background-color: #3490dc;
    color: white;
    border: none;
    padding: 5px 10px;
    cursor: pointer;
}
</style>

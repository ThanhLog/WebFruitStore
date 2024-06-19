<template>
    <div class="relative">
        <div class=" sticky bg-white top-0 left-0">
            <h2 class=" p-5 text-3xl">Quản lý đơn hàng</h2>
        </div>
        <ul class="mt-5 grid grid-cols-3 gap-4 p-5">
            <li v-for="(order, index) in orders" :key="index" class="border p-2 rounded-lg h-fit">
                <div class="flex gap-3 items-center w-full">
                    <div class=" w-full">
                        <div>Order ID: {{ order.OrderID }}</div>
                        <div class="mt-2">UserName: {{ order.UserName }}</div>
                        <div class="mt-2">Order Date: {{ formatDate(order.OrderDate) }}</div>
                        <div class="mt-2">Full Name: {{ order.FullName }}</div>
                        <div class="mt-2">Total Price: {{ order.TotalPriceOfOrder }}</div>
                        <div class="mt-2">Status: {{ order.Status }}</div>
                        <!-- Iterate over products belonging to this order -->
                        <div class="mt-4 w-full">
                            <h3 class="text-xl">Products:</h3>
                            <ul class=" w-full flex flex-col gap-3">
                                <li v-for="(product, i) in order.products" :key="i" class="p-2 border-2 w-full">
                                    <div>Product Name: {{ product.ProductName }}</div>
                                    <div>Quantity: {{ product.Quantity }}</div>
                                    <div>Price: {{ product.Price }}</div>
                                    <div>Total Price Per Product: {{ product.TotalPricePerProduct }}</div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'OrderView',
    data() {
        return {
            orders: []
        };
    },
    methods: {
        async fetchOrders() {
            try {
                const response = await axios.get('http://localhost:5022/api/Orders');
                // Group products by order ID
                const groupedOrders = {};
                response.data.forEach(order => {
                    if (!(order.OrderID in groupedOrders)) {
                        groupedOrders[order.OrderID] = {
                            ...order,
                            products: []
                        };
                    }
                    groupedOrders[order.OrderID].products.push({
                        ProductName: order.ProductName,
                        Quantity: order.Quantity,
                        Price: order.Price,
                        TotalPricePerProduct: order.TotalPricePerProduct
                    });
                });

                // Convert object to array and sort by OrderID in ascending order
                const sortedOrders = Object.values(groupedOrders).sort((a, b) => {
                    return a.OrderID - b.OrderID;
                });

                this.orders = sortedOrders;
            } catch (error) {
                console.error('Error fetching orders:', error);
            }
        },

        formatDate(dateString) {
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const date = new Date(dateString);
            return date.toLocaleDateString('vi-VN', options);
        }
    },
    mounted() {
        this.fetchOrders();
    }
};
</script>

<style scoped>
.border {
    border: 1px solid #ccc;
}

.rounded-full {
    border-radius: 50%;
}
</style>

// OrderList.vue
<template>
  <div class="order-list">
    <h2>Danh sách đơn hàng</h2>
    <div v-if="loading">Đang tải...</div>
    <div v-else>
      <div v-for="order in orders" :key="order.OrderID">
        <OrderItem :order="order" />
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import OrderItem from './OrderItem.vue';

export default {
    components: {
        OrderItem,
    },
    data() {
        return {
            orders: [],
            loading: true,
        };
    },
    mounted() {
        this.fetchOrders();
    },
    methods: {
        async fetchOrders() {
            try {
                const response = await axios.get(`http://localhost:5022/api/Orders?UserName=${this.userName}`);
                this.orders = response.data;
                this.loading = false;
            } catch (error) {
                console.error('Lỗi khi lấy danh sách đơn hàng:', error);
                this.loading = false;
            }
        },
    },
    computed: {
        userName() {
            return this.$store.state.userName || localStorage.getItem('userName');
        },
    },
};
</script>
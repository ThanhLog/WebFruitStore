<template>
    <div class="w-full bg-slate-300 rounded-md">
        <h2 class="text-xl font-semibold mb-4 pt-2 text-center">Thông tin người mua</h2>
        <form @submit.prevent="handleSubmit">
            <div class="mb-4 px-2">
                <label for="name" class="block font-medium">Họ tên:</label>
                <input type="text" id="name" v-model="localBuyerInfo.name" required
                    :class="{ 'border-red-500': !localBuyerInfo.name }"
                    class="block w-full mt-1 rounded-md border-gray-300 bg-slate-100 outline-none"
                    @input="checkFormValidity">
            </div>
            <div class="mb-4 px-2">
                <label for="phone" class="block font-medium">Số điện thoại:</label>
                <input type="tel" id="phone" v-model="localBuyerInfo.phone" required
                    :class="{ 'border-red-500': !localBuyerInfo.phone }"
                    class="block w-full mt-1 rounded-md border-gray-300 bg-slate-100 outline-none"
                    @input="checkFormValidity">
            </div>
            <div class="mb-4 px-2">
                <label for="email" class="block font-medium">Email:</label>
                <input type="email" id="email" v-model="localBuyerInfo.email" required
                    :class="{ 'border-red-500': !localBuyerInfo.email }"
                    class="block w-full mt-1 rounded-md border-gray-300 bg-slate-100 outline-none"
                    @input="checkFormValidity">
            </div>
            <div class="w-full p-3">
                <div class="bg-white p-2 rounded-lg">
                    <div class="mb-4 flex justify-between px-4">
                        <label class="block font-medium">Tổng đơn hàng:</label>
                        <span>{{ totalOrder }}</span>
                    </div>
                    <div class="mb-4 flex justify-between px-4">
                        <label class="block font-medium">VAT (10%):</label>
                        <span>{{ VAT }}</span>
                    </div>
                    <div class="mb-4 flex justify-between px-4">
                        <label class="block font-medium">Tổng phải trả:</label>
                        <span>{{ totalPayment }}</span>
                    </div>
                </div>
            </div>
            <div class="w-full text-center mt-4 pb-2">
                <button type="submit" :disabled="!isFormValid" class="bg-blue-500 text-white py-2 px-4 rounded">Đặt
                    hàng</button>
            </div>
        </form>
    </div>
</template>

<script>
export default {
    name: 'BuyerInfo',
    props: {
        buyerInfo: {
            type: Object,
            required: true
        },
        totalOrder: {
            type: Number,
            required: true
        },
        VAT: {
            type: Number,
            required: true
        },
        totalPayment: {
            type: Number,
            required: true
        }
    },
    data() {
        return {
            localBuyerInfo: { ...this.buyerInfo },
            isFormValid: false
        }
    },
    watch: {
        localBuyerInfo: {
            handler(newInfo) {
                this.$emit('updateBuyerInfo', newInfo);
                this.checkFormValidity();
            },
            deep: true
        }
    },
    methods: {
        checkFormValidity() {
            this.isFormValid = this.localBuyerInfo.name && this.localBuyerInfo.phone && this.localBuyerInfo.email;
        },
        async handleSubmit() {
            if (!this.isFormValid) return; // Kiểm tra form hợp lệ trước khi gửi đi
            const orderData = {
                userName: this.buyerInfo.userName,
                fullName: this.localBuyerInfo.name,
                products: this.$store.state.Cart.map(item => ({
                    productID: item.ProductID, // Thay thế bằng trường dữ liệu tương ứng từ sản phẩm trong giỏ hàng
                    image: item.Image, // Thay thế bằng trường dữ liệu tương ứng từ sản phẩm trong giỏ hàng
                    productName: item.ProductName,
                    quantity: item.Quantity,
                    price: item.Price,
                    totalPricePerProduct: item.Quantity * item.Price
                })),
                totalPriceOfOrder: this.totalPayment,
                status: "Chờ xác nhận"
            };
            try {
                // Gửi thông tin đơn hàng đến action createOrder thông qua sự kiện submitOrder
                await this.$store.dispatch('createOrder', orderData);
                // Nếu gửi đơn hàng thành công, chuyển hướng sang trang "/success"
                this.$router.push('/success');
            } catch (error) {
                console.error("Error submitting order:", error);
            }
        }

    }
}
</script>

<style scoped>
.border-red-500 {
    border-color: #EF4444;
}
</style>

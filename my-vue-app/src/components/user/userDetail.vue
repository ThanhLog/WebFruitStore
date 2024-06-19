<template>
    <div class="flex h-fit">
        <div class="bg-slate-300 w-1/5 h-screen">
            <div class="UserInfo flex flex-col items-center mt-5">
                <!-- Hiển thị Image của người dùng -->
                <img :src="userInfo && userInfo.length > 0 ? userInfo[0].Image : ''" alt="User Image"
                    class="bg-black rounded-full object-cover w-[150px] h-[150px]" />
                <!-- Hiển thị UserName của người dùng -->
                <p v-if="userInfo && userInfo.length > 0" class="text-2xl">{{ userInfo[0].UserName }}</p>
            </div>
            <ul class="text-center text-xl mt-6">
                <li class="mb-4 hover:bg-white">
                    <router-link to="/userDetail/account">Tài khoản</router-link>
                </li>
                <li class="mb-4 hover:bg-white">
                    <router-link to="/userDetail/order">Đơn mua</router-link>
                </li>
                <li class="mb-4 hover:bg-white">
                    <router-link to="/userDetail/help">Trợ giúp</router-link>
                </li>
            </ul>
        </div>
        <div class="w-4/5 h-screen overflow-x-auto">
            <!-- Hiển thị trang con -->
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
    computed: {
        ...mapGetters(['userInfo'])
    }
}
</script>

<style scoped>
/* Thêm các tùy chỉnh CSS tại đây */
.hover\:bg-white:hover {
    background-color: white;
}
</style>

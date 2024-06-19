<template>
    <div class="bg-cyan-500 text-white">
        <h1 class="text-center text-2xl font-semibold p-6">LOCALHOST ADMIN</h1>
        <ul class="mt-10 text-xl">
            <li :class="{ 'bg-slate-100 text-black': $route.path === '/Trang-chu' }"
                class="h-[50px] hover:bg-slate-100 hover:text-black">
                <router-link to="/Trang-chu" class="h-full flex justify-center items-center">Trang chủ</router-link>
            </li>
            <li :class="{ 'bg-slate-100 text-black': $route.path === '/Quan-ly-san-pham' }"
                class="h-[50px] hover:bg-slate-100 hover:text-black">
                <router-link to="/Quan-ly-san-pham" class="h-full flex justify-center items-center">Quản lý sản
                    phẩm</router-link>
            </li>
            <li :class="{ 'bg-slate-100 text-black': $route.path === '/Quan-ly-nguoi-dung' }"
                class="h-[50px] hover:bg-slate-100 hover:text-black">
                <router-link to="/Quan-ly-nguoi-dung" class="h-full flex justify-center items-center">Quản lý người
                    dùng</router-link>
            </li>
            <li :class="{ 'bg-slate-100 text-black': $route.path === '/Quan-ly-don-hang' }"
                class="h-[50px] hover:bg-slate-100 hover:text-black">
                <router-link to="/Quan-ly-don-hang" class="h-full flex justify-center items-center">Quản lý đơn
                    hàng</router-link>
            </li>
            <li :class="{ 'bg-slate-100 text-black': $route.path === '/Tin-nhan' }"
                class="h-[50px] hover:bg-slate-100 hover:text-black">
                <router-link to="/Tin-nhan" class="h-full flex justify-center items-center">Tin nhắn</router-link>
            </li>
        </ul>
    </div>
</template>

<script>
export default {

}
</script>

<style></style>

<template>
    <ul class="triangle-ul ml-4 mt-5 bg-white px-5">
        <li class="mb-4"><router-link to="/userDetail/account">Tài khoản</router-link></li>
        <li class="mb-4"><router-link to="/userDetail/order">Đơn mua</router-link></li>
        <li class="mb-4"><router-link to="">Đăng xuất</router-link></li>
    </ul>
</template>

<script>
export default {
    name: 'YourComponent'
}
</script>

<style scoped>
.triangle-ul {
    position: relative;
    padding: 20px;
    background-color: #fff;
    border: 1px solid #ccc;
}

.triangle-ul::before {
    content: '';
    position: absolute;
    top: -20px;
    left: 50%;
    transform: translateX(-50%);
    border-width: 0 10px 20px;
    border-style: solid;
    border-color: transparent transparent #fff transparent;
}
</style>

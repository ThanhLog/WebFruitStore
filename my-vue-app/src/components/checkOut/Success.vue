<template>
    <div class=" flex flex-col items-center">
        <svg fill="none" height="10rem" viewBox="0 0 24 24" width="10rem" xmlns="http://www.w3.org/2000/svg">
            <circle cx="12" cy="12" fill="#21c179" r="10" />
            <path clip-rule="evenodd"
                d="m16.6766 8.58327c.1936.19698.1908.51355-.0062.70708l-5.7054 5.60545c-.1914.1881-.4972.1915-.6927.0078l-2.67382-2.5115c-.20128-.189-.21118-.5055-.02212-.7067.18906-.2013.50548-.2112.70676-.0222l2.32368 2.1827 5.3628-5.26888c.1969-.19353.5135-.19073.707.00625z"
                fill="#fff" fill-rule="evenodd" />
        </svg>
        <h1 class=" mt-4 text-5xl font-mono">Đặt hàng thành công</h1>
        <p class=" mt-4 text-xl">Kiểm tra đơn hàng trong đơn mua</p>
        <router-link to="/userDetail/order"
            class=" mt-6 bg-black text-white font-semibold p-3 rounded-full flex items-center gap-2">
            Đơn mua
            <svg fill="none" height="24" viewBox="0 0 48 48" width="24" xmlns="http://www.w3.org/2000/svg">
                <path d="m0 0h48v48h-48z" fill="#fff" fill-opacity=".01" />
                <g stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-width="4">
                    <path d="m41.9999 24h-35.99998" />
                    <path d="m30 12 12 12-12 12" />
                </g>
            </svg>
        </router-link>
    </div>
</template>

<script>
export default {
    name: "Success",
}
</script>

<style></style>
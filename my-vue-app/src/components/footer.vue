<template>
    <div class=" w-full flex justify-center items-center mt-5 p-5 bg-white">
        <div class=" w-[90%] bg-[#222222] text-white p-5 rounded-2xl">
            <div class=" grid grid-cols-4 p-10">
                <div class="contact col-span-1 text-end pr-5">
                    <h2 class=" text-xl">Contact Us</h2>
                    <div class=" font-extralight mt-4">
                        <label>
                            abc.support@gmail.com
                        </label><br>
                        <label>
                            09 - 12346578
                        </label><br>
                        <label>
                            Ha Dong, Ha Noi
                        </label>
                    </div>
                </div>
                <div class=" page col-span-2 border-x text-center">
                    <h1 class=" text-3xl">Localhost</h1>
                    <p class=" text-sm mt-2">“Nguồn gốc sức khỏe, trái cây tươi sạch”</p>
                    <div class=" w-full flex justify-center mt-5 gap-5">
                        <a href="#" class=" bg-gray-500 p-2 rounded-full">
                            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24">
                                <path fill="currentColor"
                                    d="M7.8 2h8.4C19.4 2 22 4.6 22 7.8v8.4a5.8 5.8 0 0 1-5.8 5.8H7.8C4.6 22 2 19.4 2 16.2V7.8A5.8 5.8 0 0 1 7.8 2m-.2 2A3.6 3.6 0 0 0 4 7.6v8.8C4 18.39 5.61 20 7.6 20h8.8a3.6 3.6 0 0 0 3.6-3.6V7.6C20 5.61 18.39 4 16.4 4zm9.65 1.5a1.25 1.25 0 0 1 1.25 1.25A1.25 1.25 0 0 1 17.25 8A1.25 1.25 0 0 1 16 6.75a1.25 1.25 0 0 1 1.25-1.25M12 7a5 5 0 0 1 5 5a5 5 0 0 1-5 5a5 5 0 0 1-5-5a5 5 0 0 1 5-5m0 2a3 3 0 0 0-3 3a3 3 0 0 0 3 3a3 3 0 0 0 3-3a3 3 0 0 0-3-3" />
                            </svg>
                        </a>
                        <a href="#" class=" bg-gray-500 p-2 rounded-full">
                            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24">
                                <path fill="currentColor"
                                    d="M12 2.04c-5.5 0-10 4.49-10 10.02c0 5 3.66 9.15 8.44 9.9v-7H7.9v-2.9h2.54V9.85c0-2.51 1.49-3.89 3.78-3.89c1.09 0 2.23.19 2.23.19v2.47h-1.26c-1.24 0-1.63.77-1.63 1.56v1.88h2.78l-.45 2.9h-2.33v7a10 10 0 0 0 8.44-9.9c0-5.53-4.5-10.02-10-10.02" />
                            </svg>
                        </a>
                        <a href="#" class=" bg-gray-500 p-2 rounded-full">
                            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24">
                                <path fill="currentColor"
                                    d="M18.205 2.25h3.308l-7.227 8.26l8.502 11.24H16.13l-5.214-6.817L4.95 21.75H1.64l7.73-8.835L1.215 2.25H8.04l4.713 6.231zm-1.161 17.52h1.833L7.045 4.126H5.078z" />
                            </svg>
                        </a>
                    </div>
                    <div>
                        <clockVue />
                    </div>
                </div>
                <div class=" col-span-1 ml-5">
                    <h2 class=" text-xl">Trang tiện ích</h2>
                    <ul class=" font-extralight mt-4">
                        <li>Hướng dẫn mẫu</li>
                        <li>404 không tìm thấy</li>
                        <li>Mật khẩu được bảo vệ</li>
                        <li>Giấy phép</li>
                        <li>Nhật ký thay đổi</li>
                    </ul>
                </div>
            </div>
            <div class=" border-t-2 mx-5 mt-5 text-center pt-2">
                Copyright &copy; Localhost 2024 | <router-link to="">Chính sách bảo mật </router-link> | <router-link
                    to="">Điều khoản dịch vụ</router-link>
            </div>
        </div>
    </div>
</template>

<script>
import clockVue from '../components/clock.vue'
export default {
    components: { clockVue },
}
</script>

<style></style>
<template>
    <div class=" relative">
        <button @click="goBack"
            class=" absolute top-0 left-2 bg-black text-white rounded-full font-medium flex items-center px-3 py-2">
            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 15 15">
                <path fill="currentColor" fill-rule="evenodd"
                    d="M6.854 3.146a.5.5 0 0 1 0 .708L3.707 7H12.5a.5.5 0 0 1 0 1H3.707l3.147 3.146a.5.5 0 0 1-.708.708l-4-4a.5.5 0 0 1 0-.708l4-4a.5.5 0 0 1 .708 0"
                    clip-rule="evenodd" />
            </svg>Quay lại
        </button>
        <div class="w-full h-[85vh] flex justify-center items-center mt-4 -z-100">
            <div class="dashBoard w-[75%] h-[80%] rounded-lg gap-5 relative"
                style="box-shadow: 0px 15px 115px 10px rgba(0,0,0,0.1);">
                <div class=" absolute w-full">
                    <div class=" relative flex gap-[20px] items-center w-full">
                        <div class=" top-0 left-0 absolute w-[50%] z-10" v-if="showAuth === 'showSignIn'">
                            <signIn />
                        </div>
                        <div class=" top-0 right-0 absolute w-[50%] z-10" v-if="showAuth === 'showSignUp'">
                            <signUp />
                        </div>
                    </div>
                </div>
                <div class=" w-full text-white absolute top-0 h-full">
                    <div class="flex gap-[20px] h-full relative">
                        <div class="contentSignUp w-[50%] text-center rounded-r-[50px] bg-blue-900 absolute top-0 left-0 h-full"
                            v-if="showAuth === 'showSignUp'" key="showSignUp">
                            <div class=" mt-5 p-5">
                                <h2 class=" text-2xl font-semibold mb-3">Chào mừng bạn đến với chúng tôi!</h2>
                                <p class=" text-xl">Hãy tham gia cùng hàng triệu người dùng khác và khám phá thế giới
                                    của
                                    chúng
                                    tôi. Đăng ký ngay
                                    để
                                    trải nghiệm dịch vụ tốt nhất!</p>
                                <p class=" text-xl mt-3">Nếu bạn đã có tài khoản, vui lòng đăng nhập ở phía trên.</p>
                            </div>
                            <p class=" mt-5 text-xl">Bạn đã có tài khoản? <button @click="openSignIn"
                                    class=" underline">Sign
                                    In</button></p>
                        </div>
                        <div class="contentSignIn text-center w-[50%] rounded-l-[50px] bg-blue-900 absolute top-0 right-0 h-full"
                            v-if="showAuth === 'showSignIn'" key="showSignIn">
                            <div class=" mt-5 p-5">
                                <h2 class=" text-2xl font-semibold mb-3">Chào mừng bạn quay trở lại!</h2>
                                <p class=" text-xl">Cảm ơn bạn đã quay trở lại với chúng tôi. Vui lòng đăng nhập để tiếp
                                    tục
                                    trải nghiệm dịch vụ
                                    của
                                    chúng tôi.</p>
                                <p class=" text-xl mt-3">Nếu bạn chưa có tài khoản, hãy đăng ký ngay bây giờ để nhận
                                    được
                                    nhiều
                                    ưu đãi hấp dẫn!</p>
                            </div>
                            <p class=" mt-5 text-xl">Tạo tài khoản mới? <button @click="openSignUp"
                                    class=" underline">Sign
                                    Up</button></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import signIn from './signIn.vue'
import signUp from './signUp.vue'

export default {
    name: 'dashboard',
    components: {
        signIn, signUp
    },
    data() {
        return {
            showAuth: 'showSignIn',
        }
    },
    methods: {
        openSignIn() {
            this.showAuth = 'showSignIn';
        },
        openSignUp() {
            this.showAuth = 'showSignUp';
        },
        goBack() {
            this.$router.go(-1);
        }
    }
}
</script>

<style scoped></style>
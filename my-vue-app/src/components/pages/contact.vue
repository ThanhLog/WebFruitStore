<template>
    <div class=" px-10 mt-10">
        <div>
            <h1 class=" text-6xl">Liên hệ với chúng tôi</h1>
            <p class="text-xl font-normal mt-5">Chúng tôi luôn sẵn lòng lắng nghe và hỗ trợ bạn trong mọi thắc mắc và
                yêu cầu. Đừng ngần ngại liên hệ với
                chúng tôi để được tư vấn và hỗ trợ.</p>
        </div>
        <div class=" mt-3">
            <h2 class=" text-2xl font-medium">Thông tin liên hệ</h2>
            <ul class=" mt-4 line ml-5 text-base font-medium leading-loose list-disc">
                <li>Địa chỉ: <span>123 Đường Hoa, Phường Hoa Quả, Thành Phố, Quốc Gia</span></li>
                <li>Số điện thoại: <span>(+84) 943045522</span></li>
                <li>Email: <span>abc.contact@gmail.com</span></li>
            </ul>
        </div>
        <div class=" mt-3">
            <h2 class=" text-2xl font-medium">Giờ làm việc</h2>
            <ul class="mt-4 line ml-5 text-base font-medium leading-loose">
                <li>Thứ Hai - Thứ Sáu: 8:00 sáng - 5:00 chiều</li>
                <li>Thứ Bảy: 8:00 sáng - 12:00 trưa</li>
                <li>Chúng tôi nghỉ vào Chủ Nhật và các ngày lễ.</li>
            </ul>
        </div>
        <div class=" mt-5">
            <p class=" text-2xl font-semibold">Nếu bạn có bất kỳ câu hỏi hoặc yêu cầu đặc biệt, hãy liên hệ với chúng
                tôi qua điện thoại hoặc email,
                hoặc điền vào biểu mẫu liên hệ dưới đây. Chúng tôi sẽ phản hồi bạn trong thời gian sớm nhất có thể.</p>

            <!-- Form Contact -->
            <div class=" w-full flex justify-center">
                <form @submit.prevent="sendContact" class=" mt-6 w-[80%] bg-white p-3 rounded-lg shadow-lg">
                    <div class="information w-full flex flex-col">
                        <input class=" w-[40%] rounded-lg" type="text" placeholder="Họ và tên" v-model="name" /><br />
                        <input class=" w-[40%] rounded-lg" type="text" placeholder="Email" v-model="email" /><br />
                        <input class=" w-[40%] rounded-lg" type="number" placeholder="Số điện thoại" v-model="phone" />
                    </div>

                    <div class="messager mt-5">
                        <input class=" w-full" type="text" placeholder="Chủ đề" v-model="topic" /><br />
                        <textarea class="w-full mt-3 min-h-[150px]" placeholder="Nội dung" v-model="content"
                            @input="autoResize"></textarea>
                    </div>
                    <div class=" text-end mt-5">
                        <button class=" bg-lime-500 text-white text-xl px-5 py-2 font-semibold rounded-full"
                            type="submit">Gửi</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            name: '',
            email: '',
            phone: '',
            topic: '',
            content: '',
        }
    },
    mounted() {
        // Cuộn trang lên đầu khi component được mount
        window.scrollTo(0, 0);
    },
    
    methods: {
        autoResize(event) {
            const textarea = event.target;
            textarea.style.height = 'auto';
            textarea.style.height = textarea.scrollHeight + 'px';
        }
    },
}
</script>

<style scoped>
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}
</style>
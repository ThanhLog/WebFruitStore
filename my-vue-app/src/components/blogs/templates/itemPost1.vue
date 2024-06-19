<template>
    <div class="w-full flex gap-2">
        <router-link to="#" class=" w-[30%]">
            <img :src="post.imgUrl" alt="" width="100%">
        </router-link>
        <div class=" w-[70%]">
            <router-link to="#" class=" indent-5 text-3xl w-[85%] hover:text-blue-600">
                {{ post.title }}
            </router-link>
            <div class="mt-3 flex items-center gap-5">
                <p class=" flex items-center gap-2"><img :src="timeIcon" alt=""> <span>{{ post.date }}</span></p>
                <p class=" flex items-center gap-2"> <img :src="userIcon" alt="">Tác giả: {{ post.author }}</p>
            </div>
            <p class=" text-lg mt-4">{{ fiftyWords(post.content) }}</p>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            timeIcon: '../src/assets/icons/timeIcon.svg',
            userIcon: '../src/assets/icons/userIcon.svg',
        }
    },
    props: {
        post: {
            type: Object,
            required: true,
        }
    },
    methods: {
        fiftyWords(content) {
            const words = content.split(' ');
            const fiftyWords = words.slice(0, 50);
            return fiftyWords.join(' ') + (words.length > 50 ? '...' : '');
        }
    }
}
</script>

<style></style>
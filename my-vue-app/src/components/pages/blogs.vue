<template>
    <div>
        <!-- Banner -->
        <banner />
        <!-- Posts -->
        <posts />
    </div>
</template>

<script>
import banner from '../blogs/banner.vue'
import posts from '../blogs/posts.vue'
export default {
    name: 'blogs',
    components: { banner, posts,  },
    mounted() {
        // Cuộn trang lên đầu khi component được mount
        window.scrollTo(0, 0);
    },
}
</script>

<style></style>
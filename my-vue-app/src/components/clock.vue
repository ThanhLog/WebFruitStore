<!-- Clock.vue -->
<template>
    <div class="clock">
        <span>{{ currentTime }}</span>
        <span class=" ml-4">{{ currentDate }}</span>
    </div>
</template>

<script>
export default {
    data() {
        return {
            currentDate: '',
            currentTime: ''
        };
    },
    mounted() {
        this.updateTime();
        setInterval(this.updateTime, 1000);
    },
    methods: {
        updateTime() {
            const now = new Date();
            this.currentDate = now.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
            this.currentTime = now.toLocaleTimeString('en-US', {
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false,
                timeZone: 'Asia/Ho_Chi_Minh'
            });
        }
    }
};
</script>

<style scoped>
.clock {
    font-family: Arial, sans-serif;
    font-size: 18px;
    text-color: white;
    text-align: center;
    margin-top: 50px;
}
</style>

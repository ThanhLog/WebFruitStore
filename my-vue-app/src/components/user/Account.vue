<template>
    <div>

    </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
    name: 'Account',
    computed: {
        ...mapGetters(['userInfo'])
    }
}
</script>

<style></style>
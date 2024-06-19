<template>
  <div class="w-full mb-5">
    <!-- Banner -->
    <Banner />
    <!-- Main content -->
    <!-- Introduce -->
    <Introduce />
    <!-- Confirm -->
    <Confirm />
    <!-- Top Sell -->
    <topSell />
    <!-- Commit -->
    <Commit />
    <!-- Comment -->
    <Comment />
    <!-- Our Subscribe -->
    <Subscribe />
  </div>
</template>

<script>
import Banner from '../home/banner.vue'
import Confirm from '../home/Confirm.vue'
import Introduce from '../home/Introduce.vue'
import topSell from '../home/topSell.vue'
import Commit from '../home/Commit.vue'
import Comment from '../home/comment.vue'
import Subscribe from '../home/subscribe.vue'
export default {
  components: { Banner, Introduce, Confirm, topSell, Commit, Comment, Subscribe },
  mounted() {
    // Cuộn trang lên đầu khi component được mount
    window.scrollTo(0, 0);
  },
}
</script>

<style></style>
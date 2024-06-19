<template>
  <div class=" mt-5 px-5 w-full">
    <div class=" flex  flex-col gap-[50px]">
      <div class="itemPost" v-for=" (item, index) in posts" :key="item.id">
        <itemPost1 :post="item" v-if="index % 2 === 0" />
        <itemPost2 :post="item" v-if="index % 2 === 1" />
      </div>
    </div>
  </div>
</template>

<script>
import itemPost1 from './templates/itemPost1.vue'
import itemPost2 from './templates/itemPost2.vue'
export default {
  components: { itemPost1, itemPost2 },
  data() {
    return {
      posts: [
        {
          id: 1,
          imgUrl: '../src/assets/350 x 250.png',
          title: 'Post 1',
          date: '24/04/2024',
          author: 'ADMIN',
          content: `Lorem ipsum dolor sit amet consectetur, adipisicing elit.Veniam, dolor amet atque vero recusandae sunt praesentium
                  illo ipsum earum iste qui vitae labore a adipisci doloribus doloremque maiores nostrum tempore?`
        },
        {
          id: 2,
          imgUrl: '../src/assets/350 x 250.png',
          title: 'Post 2',
          date: '24/04/2024',
          author: 'ADMIN',
          content: `Lorem ipsum dolor sit amet consectetur, adipisicing elit.Veniam, dolor amet atque vero recusandae sunt praesentium
                  illo ipsum earum iste qui vitae labore a adipisci doloribus doloremque maiores nostrum tempore?`
        },
        {
          id: 3,
          imgUrl: '../src/assets/350 x 250.png',
          title: 'Post 3',
          date: '24/04/2024',
          author: 'ADMIN',
          content: `Lorem ipsum dolor sit amet consectetur, adipisicing elit.Veniam, dolor amet atque vero recusandae sunt praesentium
                  illo ipsum earum iste qui vitae labore a adipisci doloribus doloremque maiores nostrum tempore?`
        }, {
          id: 4,
          imgUrl: '../src/assets/350 x 250.png',
          title: 'Post 4',
          date: '24/04/2024',
          author: 'ADMIN',
          content: `Lorem ipsum dolor sit amet consectetur, adipisicing elit.Veniam, dolor amet atque vero recusandae sunt praesentium
                  illo ipsum earum iste qui vitae labore a adipisci doloribus doloremque maiores nostrum tempore?`
        },
      ]
    }
  },
  methods: {
    fiftyWords(content) {
      const words = content.split(' ');
      const fiftyWords = words.slice(0, 80);
      return fiftyWords.join(' ') + (words.length > 50 ? '...' : '');
    }
  },
}
</script>

<style></style>
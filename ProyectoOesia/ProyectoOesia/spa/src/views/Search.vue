<template>
  <div class="search">
    <button @click="searchItem">Prueba</button>
    <!-- <div v-for="item of search" :key=item.id>
  {{item.descripcion}}
  </div> -->
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import {searchService} from '@/services/search.service';
export default  Vue.extend({

  methods:{
    searchItem(q ="font"){
      searchService.searching(q).then(x=>{
        console.log(x);
       // this.$data.search = x;
      }).catch(error=>console.log(error));
    }

  }
})
</script>
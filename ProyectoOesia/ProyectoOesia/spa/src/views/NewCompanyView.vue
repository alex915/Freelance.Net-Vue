<template>
    <create-company  />
</template>

<script lang="ts">
import Vue from 'vue';
import CreateCompany from "@/components/CreateCompany.vue";
import { Component } from 'vue-property-decorator';
@Component({
  components: {
     "create-company": CreateCompany,
  },
})
export default class Hello extends Vue {

}
</script>
<style scoped>

</style>
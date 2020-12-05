<template>
  <img
    :src="
      'https://image.maps.ls.hereapi.com/mia/1.6/mapview?' +
      'apiKey=ukrovlB36UkfUnxL0NwTAYh1SoI2Eu2RqzyhMmyKwx0' +
      '&u=' +
      km +
      'k&z=' +
      zoom +
      '&lat=' +
      lat +
      '&lon=' +
      lon +
      '&ml=spa&nocp&t=5&w=500&h=500&pip=15&i=' +
      i +
      '&ppi=250&vt=0'
    "
  />
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
@Component
export default class StaticMap extends Vue {
  @Prop()
  public lat!: number;
  @Prop()
  public lon!: number;
  @Prop()
  public km!: number;
  @Prop()
  public i!: boolean;

  public zoom = 0;

  created() {
    this.calculateZoom();
  }
  updated() {
    this.calculateZoom();
  }

  public calculateZoom() {
    switch (this.km) {
      case 200:
        this.zoom = 7;
        break;
      case 175:
        this.zoom = 7.2;
        break;
      case 150:
        this.zoom = 7.5;
        break;
      case 125:
        this.zoom = 7.65;
        break;
      case 100:
        this.zoom = 8;
        break;
      case 75:
        this.zoom = 8.5;
        break;
      case 50:
        this.zoom = 9;
        break;
      case 25:
        this.zoom = 10;
        break;

      default:
        this.zoom = 14.5;
        break;
    }
  }
}
</script>
<style scoped>
img {
  width: 90%;
  max-width: 500px;
}
</style>
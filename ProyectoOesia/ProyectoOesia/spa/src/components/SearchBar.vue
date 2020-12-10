<template>
  <v-app-bar
    fixed
    color="#359ff4"
    dark
    hide-on-scroll
    extension-height="55"
    src=""
    prominent
    dense
    class="mleft expand"
    scroll-threshold="100"
    :floating="false"
  >
    <template v-slot:img="{ props }">
      <v-img
        v-bind="props"
        gradient="to top right, rgba(49,130,150,.5), rgba(127,181,195,.7)"
      ></v-img>
    </template>

    <template v-slot:default>
      <v-toolbar-title>Freelance.Net</v-toolbar-title>
      <v-text-field
        hide-details
        placeholder="Buscar en todas las categorias"
        append-icon="mdi-magnify"
        single-line
        clearable
        rounded
        filled
        dense
      ></v-text-field>

      <v-select
        hide-details
        class="select"
        placeholder="Seleccione actividad"
        clearable
        single-line
        rounded
        filled
        dense
        v-model="activityId"
        :items="activities"
        item-text="title"
        item-value="id"
        label="Actividad"
      ></v-select>
    </template>

    <template v-slot:extension>
      <v-btn light rounded text large :color="actFullColor" @click="fulltimed">
        <v-icon>mdi-hours-24</v-icon>
      </v-btn>
      <v-btn light rounded text large :color="actRemColor" @click="remoted">
        <v-icon>mdi-earth</v-icon>
      </v-btn>

      <v-btn
        v-if="!actRem"
        light
        rounded
        text
        large
        :color="actGeoColor"
        @click="geoActivate"
      >
        <v-icon>mdi-crosshairs-gps</v-icon>
      </v-btn>

      <v-text-field
        v-if="actGeo"
        hide-details
        placeholder="Ciudad"
        clearable
        single-line
        rounded
        filled
        dense
      ></v-text-field>

      <v-dialog v-if="!actGeo && !actRem" v-model="dialog" width="500">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="white"
            large
            light
            rounded
            text
            v-bind="attrs"
            v-on="on"
          >
            <v-icon>mdi-map-marker-distance</v-icon>
          </v-btn>
        </template>
        <div>
          <v-card>
            <v-card-title class="title"> Seleccione la distancia </v-card-title>

            <v-card-text>
              <maps
                :lat="lat"
                :lon="lng"
                :i="false"
                :km="
                  maxDistance == 25
                    ? 1
                    : maxDistance == 0
                    ? 0
                    : maxDistance - 25
                "
              />
              <v-slider
                class="mtop"
                v-model="maxDistance"
                color="primary"
                track-color="grey"
                always-dirty
                min="0"
                step="25"
                max="225"
                thumb-label="always"
                thumb-size="50"
                :tick-labels="ticks"
              >
                <template v-slot:thumb-label="{ value }">
                  {{ value == 25 ? 1 : value == 0 ? 0 : value - 25 }}km
                </template>
                <template v-slot:prepend>
                  <v-icon color="primary" @click="res"> mdi-minus </v-icon>
                </template>

                <template v-slot:append>
                  <v-icon color="primary" @click="sum"> mdi-plus </v-icon>
                </template>
              </v-slider>
              <v-divider></v-divider>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="dialog = false"> Ok </v-btn>
            </v-card-actions>
          </v-card>
        </div>
      </v-dialog>
      <v-spacer></v-spacer>
      <v-btn dark icon>
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </template>
  </v-app-bar>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import StaticMap from "./StaticMap.vue";
import { activitiesService } from "@/services/activities.service";
@Component({
  components: {
    maps: StaticMap,
  },
})
export default class SearchBar extends Vue {
  public actGeo = false;
  public actFull = false;
  public actRem = false;
  public lng = 0;
  public lat = 0;
  public activities = [];
  public activityId = 0;
  public dialog = false;
  public maxDistance = 0;
  public ticks = [
    "0",
    "1",
    "25",
    "50",
    "75",
    "100",
    "125",
    "150",
    "175",
    "200",
  ];
  public sum() {
    this.maxDistance += 25;
  }
  public res() {
    this.maxDistance -= 25;
  }

  public get actGeoColor() {
    return !this.actGeo ? "white" : "black";
  }
  public get actFullColor() {
    return this.actFull ? "white" : "black";
  }
  public get actRemColor() {
    return this.actRem ? "white" : "black";
  }
  public created() {
    this.$spinner.showSpinner();
    activitiesService
      .getActivities()
      .then((x) => (this.activities = x.data.$values))
      .catch((err) => console.log(err))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
    this.getGeoLocation();
  }

  public fulltimed() {
    this.actFull = !this.actFull;
  }
  public remoted() {
    this.actRem = !this.actRem;
  }
  public geoActivate() {
    this.actGeo = !this.actGeo;
    this.getGeoLocation();
  }
  public getGeoLocation() {
    navigator?.geolocation.getCurrentPosition(
      (pos) => {
        this.lng = pos.coords.longitude;
        this.lat = pos.coords.latitude;
      },
      (err) => {
        console.log("Activa la geoposicion");
        this.actGeo = false;
      }
    );
  }
}
</script>

<style scoped>
.mleft {
  padding-left: 56px;
  height: 213px;
}
.slider {
  width: 500px;
}
.maxwidth {
  max-width: 500px;
}
.mtop {
  margin-top: 30px;
}
.title {
  display: flex;
  justify-content: center;
  padding: 30px 0;
}
::v-deep .v-slider--horizontal .v-slider__tick .v-slider__tick-label {
  top: 10px;
  font-size: 10px;
}
::v-deep
  .v-slider--horizontal
  .v-slider__tick:last-child
  .v-slider__tick-label {
  margin-left: 12px;
}
::v-deep .v-toolbar__title {
  width: 100%;
}

@media (max-width: 700px) {
  ::v-deep .v-toolbar__content {
    flex-wrap: wrap;
    height: 150px !important;
  }
  ::v-deep .v-toolbar__extension {
    height: 50px;
  }
  .expand {
    height: 205px !important;
  }
}
@media (min-width: 700px) {
  ::v-deep .v-toolbar__content {
    flex-wrap: wrap;
    /* height: 150px !important; */
  }
 
  .select {
    max-width: 500px;
  }
}
@media (min-width: 750px) {
 ::v-deep .v-toolbar__content > .v-input{
      margin: 0 10px;
  }
}
</style>
<template>
  <section>
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
      scroll-threshold="60"
      :floating="false"
    >
      

      <template v-slot:default>
        <v-toolbar-title >Freelance.Net</v-toolbar-title>
        <v-text-field
          hide-details
          placeholder="Buscar en todas las categorias"
          append-icon="mdi-magnify"
          single-line
          clearable
          rounded
          filled
          dense
          v-model="search"
          @change="searchAction"
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
          @change="searchAction"
        ></v-select>
      </template>

      <template v-slot:extension>
        <v-btn
          light
          rounded
          text
          large
          :color="actFullColor"
          @click="fulltimed"
        >
          <v-icon>mdi-hours-24</v-icon>
        </v-btn>
        <v-btn light rounded text large :color="actRemColor" @click="remoted">
          <v-icon>mdi-earth</v-icon>
        </v-btn>

        <v-btn
          v-if="!remote"
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
          v-if="!actGeo"
          hide-details
          placeholder="Ciudad"
          clearable
          single-line
          rounded
          filled
          dense
          v-model="city"
           @change="searchAction"
        ></v-text-field>

        <v-dialog v-if="actGeo && !remote" v-model="dialog" width="500">
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
              <v-card-title class="title">
                Seleccione la distancia
              </v-card-title>

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
                <v-btn color="primary" text @click="close"> Ok </v-btn>
              </v-card-actions>
            </v-card>
          </div>
        </v-dialog>
        <v-spacer></v-spacer>
        <v-btn dark icon @click="clear">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-app-bar>
    <v-container class="mutop">
      <v-row>
        <v-col v-if="companies.lenght > 0">No hay datos</v-col>
        <v-col v-for="company in companies" :key="company.id">
          <company-card :company="company"></company-card>
        </v-col>
      </v-row>
    </v-container>
  </section>
</template>

<script lang="ts">
import CompanyCard from "@/components/CompanyCard.vue";
import { Component, Vue, Watch } from "vue-property-decorator";
import { searchService } from "@/services/search.service";
import { AxiosResponse } from "axios";
import StaticMap from "@/components/StaticMap.vue";
import { activitiesService } from "@/services/activities.service";

@Component({
  components: {
    "company-card": CompanyCard,
    maps: StaticMap,
  },
})
export default class Admin extends Vue {
  public companies: any[] = [];
  public actGeo = true;
  public lng = 0;
  public lat = 0;
  public maxDistance = 225;
  public activityId = 0;
  public search = "";
  public city = "";
  public remote = false;
  public fulltime = false;
  public activities = [];
  public dialog = false;
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
    return this.actGeo ? "white" : "grey";
  }
  public get actFullColor() {
    return this.fulltime ? "white" : "grey";
  }
  public get actRemColor() {
    return this.remote ? "white" : "grey";
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
    this.fulltime = !this.fulltime;
    this.searchAction();
  }
  public remoted() {
    this.remote = !this.remote;
    this.searchAction();
  }
  public geoActivate() {
    this.actGeo = !this.actGeo;
    if(this.actGeo) {
      this.getGeoLocation();
    }
    else {
      this.searchAction();
    }
  }
  public getGeoLocation() {
    navigator?.geolocation.getCurrentPosition(
      (pos) => {
        this.lng = pos.coords.longitude;
        this.lat = pos.coords.latitude;
        this.searchAction();
      },
      (err) => {
        console.log("Activa la geoposicion");
        this.actGeo = false;
        this.searchAction();
      }
    );
  }
  public clear() {
    this.maxDistance = 225;
    this.activityId = 0;
    this.search = "";
    this.city = "";
    this.remote = false;
    this.fulltime = false;
    this.searchAction();
  }
  public close() {
    this.dialog = false;
    this.searchAction();
  }
  public searchAction() {
    this.$spinner.showSpinner();
    let params = "";
    if (this.search != "") {
      params += "&search=" + this.search;
    }
    if (this.city) {
      params += "&city=" + this.city;
    }
    if (this.activityId != undefined && this.activityId != 0) {
      params += "&activityId=" + this.activityId;
    }
    if(this.lat && this.actGeo) {
      params += "&lat=" + this.lat + "&lng=" + this.lng;
      params +=
      "&maxDistance=" +
      (this.maxDistance == 25
        ? 1
        : this.maxDistance == 0
        ? 0
        : this.maxDistance - 25);
    }
    
    if (this.remote) {
      params += "&remote=true";
    }
    if (this.fulltime) {
      params += "&fulltime=true";
    }
    params = params.slice(1);
    console.log(params);
    searchService
      .searching(params)
      .then((x) => {
        this.companies = x.data.$values;
        console.log(x.data.$values);
      })
      .catch((x) => (this.companies = []))
      .finally(this.$spinner.removeSpinner());
  }
}
</script>

<style lang="css" scoped>
.mutop {
  margin-top: 180px;
}
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
.titleLogo {
  font-family: "Nothing You Could Do", cursive;
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
  ::v-deep .v-toolbar__content > .v-input {
    margin: 0 10px;
  }
}
@import url("https://fonts.googleapis.com/css2?family=Nothing+You+Could+Do&display=swap");
</style>
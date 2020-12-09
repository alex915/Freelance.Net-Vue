<template v-slot:prepend-item>
<div>
  <v-list-item>
    <v-list-item-content>
      <v-text-field v-model="searchTerm" placeholder="Search" @input="search"></v-text-field>
    </v-list-item-content>
  </v-list-item>
  <v-card>
    <v-container fluid>
      <v-row
        align="center"
      >
        <v-col cols="4">
          <v-autocomplete
            v-model="maximaDistancia"                  
            label="Maxima distancia lugar de trabajo por provincia"
          ></v-autocomplete>
          </v-col>
      </v-row>
    </v-container>
  </v-card>
   <v-card>
    <v-container fluid>
      <v-row
        align="center"
      >
        <v-col cols="4">
          <v-autocomplete
            v-model="Provincia"                  
            label="Provincia"
          ></v-autocomplete>
          </v-col>
      </v-row>
    </v-container>
  </v-card>
   <v-card>
    <v-container fluid>
      <v-row
        align="center"
      >
        <v-col cols="4">
          <v-autocomplete
            v-model="Ciudad"                  
            label="Ciudad"
          ></v-autocomplete>
          </v-col>
      </v-row>
    </v-container>
  </v-card>
  <v-card>
    <v-container fluid>
      <v-row
        align="center"
      >
        <v-col cols="4">
          <v-autocomplete
            v-model="BusquedaCompañias"
            label="Busqueda por descripcion de las compañias"         
          ></v-autocomplete>
          </v-col>
      </v-row>
    </v-container>
  </v-card>
   <v-col cols="6">
    <v-switch
    v-model="fullTime"
    label="Trabajo de urgencias 24h"
    ></v-switch>
    </v-col>
  
</div>
</template>


<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import { searchService } from "@/services/search.service";
import { locationService } from "@/services/locations.service";
import { activitiesService } from "@/services/activities.service";

@Component
export default class Search extends Vue {
public search = "";
public fullTime = false;
public maximaDistancia = -1;
public provincia = "";
public ciudad = "";
public busquedaCompañias = "";
  provinceList: any;
  activities: any;
  maxDistance: number|undefined;

 public mounted() {
    locationService
      .getProvince()
      .then((x) => (this.provinceList = x.data.result.items))
      .catch((err) => console.log(err));
    activitiesService
      .getActivities()
      .then((x) => (this.activities = x.data.$values))
      .catch((err) => console.log(err));
  }
 public get token(): string {
    return this.$store.getters["getToken"];
  }
  public sum() {
    this.maxDistance += 25;
  }
  public res() {
    this.maxDistance -= 25;
  }

}



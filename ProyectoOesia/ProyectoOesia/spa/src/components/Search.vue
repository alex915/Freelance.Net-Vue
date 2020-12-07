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
            v-model="MaximaDistancia"                  
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

@Component
export default class Search extends Vue {
public search = "";


}



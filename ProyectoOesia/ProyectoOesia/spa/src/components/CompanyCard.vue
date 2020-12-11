<template>
  <v-card class="mx-auto" max-width="450">
    <v-img
      :src="'data:image/jpg;base64,' + company.image"
      height="150px"
    ></v-img>

    <v-card-title>
      {{ company.companyName }}
    </v-card-title>

    <v-card-subtitle class="text-align-left">
      {{ company.description }}
    </v-card-subtitle>
    <div class="text-align-left flex">
      <a class="no__dec" text :href="'mailTo:' + company.email">
        {{ company.email }}
      </a>
      <a class="no__dec" text :href="'tel:+' + company.ext + company.phone">
        {{ company.phone }}
      </a>
    </div>

    <v-card-subtitle>
      <v-chip-group column>
        <v-chip v-if="company.fullTime" color="white--text orange">
          24 horas
        </v-chip>
        <v-chip v-if="company.maxDistance == -1" color="white--text light-blue">
          Remoto
        </v-chip>
      </v-chip-group>
    </v-card-subtitle>

    <v-card-actions>
      
      <v-btn color="blue" @click="contact" text>Contacto</v-btn>

      <v-spacer></v-spacer>

      <v-btn icon @click="show = !show">
        <v-icon>{{ show ? "mdi-chevron-up" : "mdi-chevron-down" }}</v-icon>
      </v-btn>
    </v-card-actions>

    <v-expand-transition>
      <div v-show="show">
        <v-divider></v-divider>

        <v-card-text>
          <v-spacer></v-spacer>
          <maps
            :lat="company.latitude"
            :lon="company.longitude"
            :km="
              company.maxDistance == 25
                ? 1
                : company.maxDistance == 0
                ? 0
                : company.maxDistance - 25
            "
            :i="true"
            class="margins"
          />
        </v-card-text>
      </div>
    </v-expand-transition>
  </v-card>
</template>

<script lang="ts">
import { contactsService } from "@/services/contacts.service";
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import StaticMap from "./StaticMap.vue";

@Component({
  components: {
    maps: StaticMap,
  },
})
export default class CompanyCard extends Vue {
  @Prop({ required: true })
  public company: any;
  public show = false;
  public contact() {
    this.$spinner.showSpinner();
    contactsService
      .contact(this.company.id)
      .then((x) => console.log("Contacted"))
      .finally(this.$spinner.removeSpinner());
  
  }
}
</script>

<style>
a.no__dec{
  color:black;
  text-decoration: none;
}
a.no__dec:hover{
  color:orange;
}
.text-align-left {
  text-align: left;
  padding-bottom: 0;
}
.flex {
  display: flex;
  flex-direction: column;
  padding: 18px;
  
}

.v-slide-group__content {
  justify-content: center;
}
</style>
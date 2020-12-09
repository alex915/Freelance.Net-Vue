<template>
  <div class="padding">
    <v-stepper v-model="em" vertical>
      <v-row justify="center" class="pb-6">
        <v-col cols="12">
          <h4>Crea tu empresa</h4>
        </v-col>
      </v-row>
      <v-stepper-step step="1">Datos de la empresa</v-stepper-step>

      <v-stepper-content step="1">
        <v-row>
          <v-col cols="12" lg="3">
            <v-text-field
              :rules="[requiredRules]"
              v-model="cif"
              :outlined="true"
              label="Código Identificativo"
              hint="Introduzca su CIF, NIF, NIE..."
            ></v-text-field>
          </v-col>
          <v-col cols="12" lg="4">
            <v-text-field
              v-model="nameCompany"
              :rules="[requiredRules]"
              :outlined="true"
              label="Empresa"
              hint="Introduzca el nombre de su empresa"
            ></v-text-field>
          </v-col>
          <v-col cols="12" lg="5">
            <v-select
              v-model="activityId"
              :rules="[requiredRules]"
              :outlined="true"
              :items="activities"
              item-text="title"
              item-value="id"
              label="Actividad"
              hint="Introduzca el nombre de su empresa"
            ></v-select>
          </v-col>
          <v-col cols="12">
            <v-btn color="primary" @click="em++">Siguiente</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>

      <v-stepper-step step="2">Datos de Localizacion</v-stepper-step>

      <v-stepper-content step="2">
        <v-row>
          <v-col cols="12" sm="8" md="12" lg="4" order-sm="1">
            <v-text-field
              v-model="address"
              :rules="[requiredRules]"
              :outlined="true"
              label="Dirección"
              hint="Introduzca la dirección"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="7" md="12" lg="3" order-sm="3" order-md="2">
            <v-text-field
              v-model="city"
              :rules="[requiredRules]"
              :outlined="true"
              label="Ciudad"
              hint="Introduzca la ciudad"
            ></v-text-field>
          </v-col>
          <v-col cols="7" sm="4" md="7" lg="3" order-sm="2" order-md="3">
            <v-select
              :items="provinceList"
              label="Provincia"
              :rules="[requiredRules]"
              hint="Introduzca la provincia"
              item-text="label"
              item-value="label"
              v-model="province"
              outlined
            ></v-select>
          </v-col>
          <v-col cols="5" lg="2" order-sm="4">
            <v-text-field
              v-model="pc"
              :rules="[requiredRules]"
              :outlined="true"
              label="Codigo Postal"
              hint="Introduzca el código postal"
            ></v-text-field>
          </v-col>
          <v-col order-sm="5">
            <v-btn text @click="em--">Volver</v-btn>
            <v-btn color="primary" @click="em++">Siguiente</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>

      <v-stepper-step step="3"> Datos de contacto </v-stepper-step>

      <v-stepper-content step="3">
        <v-row>
          <v-col cols="4" sm="3" lg="2">
            <v-text-field
              v-model="ext"
              :rules="[requiredRules]"
              :outlined="true"
              label="Codigo pais"
              prefix="+"
              hint="Introduzca el código del pais"
            ></v-text-field>
          </v-col>
          <v-col cols="8" sm="9" lg="3">
            <v-text-field
              v-model="phone"
              :rules="[requiredRules]"
              :outlined="true"
              label="Telefono"
              hint="Introduzca el teléfono de contacto"
            ></v-text-field>
          </v-col>
          <v-col cols="12" lg="6">
            <v-text-field
              v-model="email"
              :rules="[requiredRules]"
              :outlined="true"
              label="Email contacto"
              hint="Introduzca el correo electrónico de contacto"
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-btn text @click="em--">Volver</v-btn>
            <v-btn color="primary" @click="em++">Siguiente</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>

      <v-stepper-step step="4">
        Datos sobre el trabajo realizado
      </v-stepper-step>

      <v-stepper-content step="4">
        <v-row class="start">
          <v-col cols="12" sm="6">
            <v-switch v-model="remoteWork" label="Trabajo remoto"></v-switch>
          </v-col>
          <v-col cols="12" sm="6">
            <v-switch
              v-model="fullTime"
              label="Trabajo de urgencias 24h"
            ></v-switch>
          </v-col>

          <v-col cols="12" v-if="!remoteWork" class="column">
            <v-subheader class="margins"
              >Introduzca la distancia máxima a desplazarse</v-subheader
            >

            <v-slider
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
          </v-col>
          <v-col cols="12">
            <v-textarea
              v-model="description"
              :rules="[requiredRules]"
              :outlined="true"
              counter
              maxlength="500"
              label="Descripción"
              hint="Introduzca una descripción de su empresa y los servicios que ofrece"
            ></v-textarea>
          </v-col>
          <v-col>
            <v-btn text @click="em--">Volver</v-btn>
            <v-btn color="primary" @click="em++">Siguiente</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>

      <v-stepper-step step="5"
        >Imagen y Redes Sociales <small>Opcional</small></v-stepper-step
      >

      <v-stepper-content step="5">
        <v-row align="center">
          <v-col cols="12" lg="6">
            <v-file-input
              :multiple="false"
              :show-size="true"
              label="Imagen"
              append-icon="mdi-camera"
              prepend-icon=""
              accept="image/png, image/jpeg"
              placeholder="Selecciona imagen"
              outlined
              @change="handleInput"
            ></v-file-input>
          </v-col>
          <v-col cols="12" lg="6" v-if="image"><v-img :src="url" height="25vh" ></v-img></v-col>
          <v-col cols="12">
            <v-btn text @click="em--">Volver</v-btn>
            <v-btn color="primary" @click="em++">Siguiente</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>

      <v-stepper-step step="6">Resumen</v-stepper-step>

      <v-stepper-content step="6">
        <v-img :src="url" height="150px"></v-img>

        <v-row>
          <v-col cols="12" lg="3">
            <v-text-field
              :rules="[requiredRules]"
              v-model="cif"
              :outlined="false"
              label="Código Identificativo"
              hint="Introduzca su CIF, NIF, NIE..."
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" lg="4">
            <v-text-field
              v-model="nameCompany"
              :rules="[requiredRules]"
              :outlined="false"
              label="Empresa"
              hint="Introduzca el nombre de su empresa"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" lg="5">
            <v-select
              v-model="activityId"
              :outlined="false"
              :items="activities"
              item-text="title"
              item-value="id"
              label="Actividad"
              hint="Introduzca el nombre de su empresa"
              disabled
            ></v-select>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="address"
              :outlined="false"
              label="Dirección"
              hint="Introduzca la dirección"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="3">
            <v-text-field
              v-model="city"
              :outlined="false"
              label="Ciudad"
              hint="Introduzca la ciudad"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="6" md="3">
            <v-text-field
              label="Provincia"
              v-model="province"
              :outlined="false"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="6" md="2">
            <v-text-field
              v-model="pc"
              :outlined="false"
              label="Codigo Postal"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="4" lg="1">
            <v-text-field
              v-model="ext"
              :outlined="false"
              label="Codigo pais"
              prefix="+"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="8" lg="2">
            <v-text-field
              v-model="phone"
              :outlined="false"
              label="Telefono"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6" lg="5">
            <v-text-field
              v-model="email"
              :outlined="false"
              label="Email contacto"
              disabled
            ></v-text-field>
          </v-col>
          <v-col cols="6" md="3" lg="2" v-if="remoteWork">
            <v-switch
              v-model="remoteWork"
              label="Sin salida"
              disabled
            ></v-switch>
          </v-col>
          <v-col cols="6" md="3" lg="2">
            <v-switch v-model="fullTime" label="24h" disabled></v-switch>
          </v-col>
          <v-col cols="4" md="3" lg="2">
            <v-text-field
              v-if="!remoteWork"
              v-model="maxDistance"
              :rules="[requiredRules]"
              :outlined="false"
              prefix="km"
              label="Distancia máxima"
              hint="Introduzca la distancia máxima a desplazarse"
              disabled
              reverse
            ></v-text-field>
          </v-col>
          <v-col cols="12">
            <v-textarea
              v-model="description"
              :rules="[requiredRules]"
              :outlined="false"
              label="Descripción"
              hint="Introduzca una descripción de su empresa y los servicios que ofrece"
              disabled
            ></v-textarea>
          </v-col>
          <v-col>
            <v-btn text @click="em--">Volver</v-btn>
            <v-btn color="primary" @click="createCompany">Registrar</v-btn>
          </v-col>
        </v-row>
      </v-stepper-content>
    </v-stepper>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import { locationService } from "@/services/locations.service";
import { activitiesService } from "@/services/activities.service";
import { companiesService } from "@/services/companies.service";
import { authService } from "@/services/auth.service";

@Component
export default class CreateCompany extends Vue {
  public isValid = false;
  public cif = "";
  public nameCompany = "";
  public activities = [];
  public activityId = 0;
  public address = "";
  public city = "";
  public province = "";
  public provinceList = [];
  public pc = "";
  public country = "España";
  public ext = "";
  public phone = "";
  public email = "";
  public maxDistance = -1;
  public em = 1;
  public description = "";
  public image = "";
  public availability = true;
  public fullTime = true;
  public remoteWork = true;
  public requiredRules = (v: string) => !!v || "Required";
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
  public get url() {
    if (this.image) {
      return URL.createObjectURL(this.image);
    }
    return "";
  }
  public mounted() {
    this.$spinner.showSpinner();
    locationService
      .getProvince()
      .then((x) => (this.provinceList = x.data.result.items))
      .catch((err) => console.log(err))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
    this.$spinner.showSpinner();
    activitiesService
      .getActivities()
      .then((x) => (this.activities = x.data.$values))
      .catch((err) => console.log(err))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
  }
  public sum() {
    this.maxDistance += 25;
  }
  public res() {
    this.maxDistance -= 25;
  }

  private handleInput(input: any) {
    if (input.length > 0) {
      let fileName = "";
      fileName = input[0].name;
      if (fileName.lastIndexOf(".") <= 0) {
        return;
      }
      const fr = new FileReader();
      fr.readAsDataURL(input[0]);
      fr.addEventListener("load", () => {
        this.image = input[0];
      });
    } else if (input.length === 0) {
      this.image = "";
    }
  }

  public createCompany() {
    this.$spinner.showSpinner();
    const company = {
      Identifier: this.cif,
      CompanyName: this.nameCompany,
      Address: this.address,
      City: this.city,
      Province: this.province,
      Pc: this.pc,
      Country: this.country,
      Ext: this.ext,
      Phone: this.phone,
      Email: this.email,
      ActivityId: this.activityId,
      Image: this.image,
      MaxDistance: this.maxDistance,
      Description: this.description,
      Availability: this.availability,
      FullTime: this.fullTime,
    };
    companiesService
      .createCompany(company)
      .then(() => {
        this.$spinner.showSpinner();
        authService
          .getUser()
          .then((x) => {
            this.$store.dispatch("setUser", x.data);
          })
          .finally(this.$spinner.removeSpinner());
        this.$router.push({ name: "Home" });
      })
      .catch((x) => console.log(x))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
  }
}
</script>
<style scoped>
.v-stepper--vertical .v-stepper__content {
  padding: 100px 120px 10px 90px;
  display: flex;
  justify-content: center;
}
.v-stepper--vertical,
.v-stepper {
  padding: 130px 70px 30px;
}
.row {
  padding: 40px 0;
}
.v-btn {
  margin: 0 15px;
}
.v-stepper__wrapper div {
  display: flex;
  justify-content: center;
}
.padding {
  padding: 10px;
}
.column {
  flex-direction: column;
}
.margins {
  margin-bottom: 50px;
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
@media (max-width: 950px) {
  .v-stepper--vertical .v-stepper__content {
    padding: 61px 60px 1px 22px;
  }
  .v-stepper--vertical,
  .v-stepper {
    padding: 100px 1px 30px;
  }
}
@media (max-width: 720px) {
  .v-stepper__wrapper div.start div {
    display: flex;
    justify-content: flex-start;
    padding-left: 10px;
  }
}
</style>
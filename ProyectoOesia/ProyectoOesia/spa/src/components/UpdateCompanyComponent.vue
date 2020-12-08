<template>
  <div class="padding">
    <div class="box">
          <v-img
      :src="'data:image/jpg;base64,'+ image" 
      height="20vh"
      class="mb-16"
    ></v-img>
        <v-col cols="12">
          <h4>Actualiza tu empresa</h4>
        </v-col>
      <v-row class="px-16">
        <v-col cols="12" lg="3">
          <v-text-field
            :rules="[requiredRules]"
            v-model="cif"
            outlined
            label="Código Identificativo"
            hint="Introduzca su CIF, NIF, NIE..."
          ></v-text-field>
        </v-col>
        <v-col cols="12" lg="4">
          <v-text-field
            v-model="nameCompany"
            :rules="[requiredRules]"
            outlined
            label="Empresa"
            hint="Introduzca el nombre de su empresa"
          ></v-text-field>
        </v-col>
        <v-col cols="12" lg="5">
          <v-select
            v-model="activityId"
            outlined
            :items="activities"
            item-text="title"
            item-value="id"
            :rules="[requiredRules]"
            label="Actividad"
            hint="Introduzca el nombre de su empresa"
          ></v-select>
        </v-col>

        <v-col cols="12" sm="7" md="12" lg="4">
          <v-text-field
            v-model="address"
            outlined
            :rules="[requiredRules]"
            label="Dirección"
            hint="Introduzca la dirección"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="5" lg="3">
          <v-text-field
            v-model="city"
            outlined
            :rules="[requiredRules]"
            label="Ciudad"
            hint="Introduzca la ciudad"
          ></v-text-field>
        </v-col>
        <v-col cols="8" md="5" lg="3">
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
        <v-col cols="4" md="2">
          <v-text-field
            v-model="pc"
            outlined
            :rules="[requiredRules]"
            label="Codigo Postal"
            hint="Introduzca el código postal"
          ></v-text-field>
        </v-col>
        <v-col cols="3" md="2" lg="1">
          <v-text-field
            v-model="ext"
            outlined
            :rules="[requiredRules]"
            label="Codigo pais"
            prefix="+"
            hint="Introduzca el código del pais"
          ></v-text-field>
        </v-col>
        <v-col cols="9" md="3" lg="2">
          <v-text-field
            v-model="phone"
            outlined
            :rules="[requiredRules]"
            label="Telefono"
            hint="Introduzca el teléfono de contacto"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="7" lg="4">
          <v-text-field
            v-model="email"
            outlined
            :rules="[requiredRules]"
            label="Email contacto"
            hint="Introduzca el correo electrónico de contacto"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12" lg="5">
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
          >
          </v-file-input>
        </v-col>
        <v-col cols="12">
          <div class="switch">
            <v-switch v-model="fullTime" label="24h"></v-switch>

            <v-switch v-model="remoteWork" label="Trabajo remoto"></v-switch>
          </div>
        </v-col>

        <v-col cols="12" v-if="!remoteWork">
          <v-subheader
            >Introduzca la distancia máxima a desplazarse</v-subheader
          >
          <maps
            :lat="lat"
            :lon="lon"
            :km="
              maxDistance == 25 ? 1 : maxDistance == 0 ? 0 : maxDistance - 25
            "
            :i="false"
            class="margins"
          />
        </v-col>
        <v-col class="mt-8 mb-12" cols="12" v-if="!remoteWork">
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
            outlined
            label="Descripción"
            hint="Introduzca una descripción de su empresa y los servicios que ofrece"
          ></v-textarea>
        </v-col>
        <div class="btnCenter">
          <v-btn color="primary" @click="updateCompany">Guardar cambios</v-btn>
          <v-btn v-if="!availability" color="success" @click="disableCompany"
            >Activar</v-btn
          >
          <v-btn v-if="availability" color="warning" @click="disableCompany"
            >Desactivar temporalmente</v-btn
          >
          <v-btn color="error" @click="dialogDelete = true">Dar de baja</v-btn>
        </div>
      </v-row>
    </div>
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="headline"
          >Estás seguro que quieres borrar la empresa?</v-card-title
        >
        <v-spacer></v-spacer>
        <v-card-subtitle>Este proceso es irreversible.</v-card-subtitle>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeDelete"
            >Cancelar</v-btn
          >
          <v-btn color="blue darken-1" text @click="deleteItemConfirm"
            >OK</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Watch } from "vue-property-decorator";
import { locationService } from "@/services/locations.service";
import { activitiesService } from "@/services/activities.service";
import { companiesService } from "@/services/companies.service";
import StaticMap from "./StaticMap.vue";
import { usersService } from "@/services/users.service";

@Component({
  components: {
    maps: StaticMap,
  },
})
export default class UpdateCompanyComponent extends Vue {
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
  public description = "";
  public availability = false;
  public fullTime = true;
  public remoteWork = true;
  public id = 0;
  public userId = "";
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
  public user: any;
  public lat = 0;
  public lon = 0;

  public dialogDelete = false;
  public image = "";

  @Watch("dialogDelete")
  public dialogDeleteWatch(val: any) {
    val || this.closeDelete();
  }
  public closeDelete() {
    this.dialogDelete = false;
  }
  public deleteItemConfirm() {
    this.$spinner.showSpinner();
    companiesService
      .deleteCompany(this.id)
      .then((response: any) => {
        this.$router.push({ name: "Login" });
      })
      .finally(this.$spinner.removeSpinner());
    this.closeDelete();
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
  public mounted() {
    this.$spinner.showSpinner();
    locationService
      .getProvince()
      .then((x) => (this.provinceList = x.data.result.items))
      .catch((err) => console.log(err))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
    activitiesService
      .getActivities()
      .then((x) => (this.activities = x.data.$values))
      .catch((err) => console.log(err))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
    this.getCompanyData();
  }
  public sum() {
    this.maxDistance += 25;
  }
  public res() {
    this.maxDistance -= 25;
  }
  public disableCompany() {
    this.$spinner.showSpinner();
    const company = this.getCompanyObjectData();
    company.availability = !company.availability;

    companiesService
      .updateCompany(company)
      .then((x) => {
        console.log(x.data);
        this.user.company = company;
        this.$store.dispatch("setUser", this.user);
        this.availability = !this.availability;
        this.$router.push({ name: "UpdateUser" });
      })
      .catch((x) => console.log(x))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
  }
  public updateCompany() {
    this.$spinner.showSpinner();
    const company = this.updateCompanyObject();
    companiesService
      .updateCompany(company)
      .then((x) => {
        console.log(x.data);
        this.$router.push({ name: "Home" });
      })
      .catch((x) => console.log(x))
      .finally(() => {
        this.$spinner.removeSpinner();
      });
  }

  public getCompanyData(): void {
    this.user = this.$store.getters["user"];
    const company = this.user.company;
    this.id = company.id;
    this.userId = company.userId;
    this.cif = company.identifier;
    this.nameCompany = company.companyName;
    this.activities = [];
    this.activityId = company.activityId;
    this.address = company.address;
    this.city = company.city;
    this.province = company.province;
    this.provinceList = [];
    this.pc = company.pc;
    this.country = company.country;
    this.ext = company.ext;
    this.phone = company.phone;
    this.email = company.email;
    this.lat = company.latitude;
    this.lon = company.longitude;
    this.maxDistance = company.maxDistance;
    this.description = company.description;
    this.availability = company.availability;
    this.fullTime = company.fullTime;
    this.image = company.image;
    this.remoteWork = company.maxDistance == -1;
  }

  public getCompanyObjectData(): any {
    this.user = this.$store.getters["user"];
    const company = this.user.company;
    const companyObj = {
      id: company.id,
      userId: company.userId,
      identifier: company.identifier,
      companyName: company.companyName,
      address: company.address,
      city: company.city,
      province: company.province,
      pc: company.pc,
      country: company.country,
      ext: company.ext,
      phone: company.phone,
      email: company.email,
      lat: company.latitude,
      lon: company.longitude,
      activityId: company.activityId,
      maxDistance: company.maxDistance,
      description: company.description,
      availability: company.availability,
      image:company.image,
      fullTime: company.fullTime,
    };
    return companyObj;
  }

  public updateCompanyObject(): any {
    const company = {
      id: this.id,
      userId: this.userId,
      identifier: this.cif,
      companyName: this.nameCompany,
      address: this.address,
      city: this.city,
      province: this.province,
      pc: this.pc,
      country: this.country,
      ext: this.ext,
      phone: this.phone,
      email: this.email,
      activityId: this.activityId,
      maxDistance: this.remoteWork ? -1 : this.maxDistance,
      description: this.description,
      availability: this.availability,
      image : this.image,
      fullTime: this.fullTime,
    };
    return company;
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
  padding: 40px 20px;
}
.v-btn {
  margin: 10px 15px;
}
.margins {
  margin: 40px 0;
}
.v-stepper__wrapper div {
  display: flex;
  justify-content: center;
}
.padding {
  padding: 10px;
}
.switch {
  justify-content: space-around;
  display: flex;
}
.btnCenter {
  display: flex;
  justify-content: center;
  width: 100%;
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
.box {
  background: white;
  box-shadow: 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 2px 2px 0 rgba(0, 0, 0, 0.14),
    0 1px 5px 0 rgba(0, 0, 0, 0.12);
}
@media (max-width: 900px) {
  .btnCenter {
    display: grid;
    width: 100%;
    justify-content: stretch;
  }
}
</style>
<template>
  <div>
    <v-carousel
      class="carrusel"
      cycle
      height="300"
      hide-delimiter-background
      :show-arrows="false"
    >
      <v-carousel-item v-for="(slide, i) in slides" :key="i">
        <v-sheet :color="colors[i]" height="100%" :class="'slide-' + i">
        </v-sheet>
      </v-carousel-item>
    </v-carousel>
    <v-app-bar
      fixed
      color="#359ff4"
      dark
      dense
      class="mleft expand"
      scroll-threshold="60"
      :floating="false"
    >
      <v-spacer></v-spacer>

      <v-toolbar-title>Freelance.Net</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>
    <div class="mitop">
      <v-card>
        <v-card-title> Bienvenidos a Freelance.Net. </v-card-title>
        <v-card-text>
          En esta página podra encontrar los trabajadores necesarios para su
          proyecto. Pulse en buscar contactos para comenzar. Podrá filtrar por
          actividad, por urgencias, por ubicación, e incluso podrá encontrar
          trabajadores que ofrezcan sus servicios en remoto. Una vez encontrada
          la empresa, podrá contactar directamente con ellos, o bien, mandar una
          notificación de contacto, de esta forma, se pondrán en contacto con
          usted.
        </v-card-text>
        <v-card-actions class="flexi" v-if="!token">
          <v-btn class="buton" @click="register" color="primary"
            >REGISTRARSE</v-btn
          >
          <v-btn class="buton" @click="login" color="success"
            >INICIAR SESIÓN</v-btn
          >
        </v-card-actions>
      </v-card>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";

@Component({
  components: {},
})
export default class Home extends Vue {
  public colors = [
    "indigo",
    "warning",
    "pink darken-2",
    "red lighten-1",
    "deep-purple accent-4",
  ];
  public slides = ["", "", "", "", ""];
  public model = null;
  public get hasCompany(): boolean {
    return this.$store.getters["user"]?.company?.id != -1;
  }
  get token(): any {
    return this.$store.getters["token"];
  }
  public created() {
    if (!this.hasCompany) {
      this.$router.push({ name: "New Company" });
    }
  }
  public register() {
    this.$router.push({ name: "Register" });
  }
  public login() {
    this.$router.push({ name: "Login" });
  }
}
</script>

<style scoped>
.carrusel {
  position: fixed;
  left: 0;
  right: 0;
  top: 45px;
}
.roof {
  margin-top: 300px;
}
.mleft {
  padding-left: 56px;
  height: 213px;
}
.mitop {
  margin-top: 400px;
}
.flexi {
  display: flex;
  justify-content: space-evenly;
  height: 180px;
}
.v-card__actions > .v-btn.v-btn.buton {
  padding: 15px 55px;
}

.slide-0 {
  background-image: url("https://www.wellnesshsf.com/wp-content/uploads/sites/3646/2017/01/banner1.jpg");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}

.slide-1 {
  background-image: url("https://tbcdn.talentbrew.com/company/138/v1_0/img/img-vet-with-medicine.jpg");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}
.slide-2 {
  background-image: url("https://www.dennisexpress.com/wp-content/uploads/2016/09/chef-banner-cooking-1280-1.jpg");
  background-position: top;
  background-repeat: no-repeat;
  background-size: cover;
}
.slide-3 {
  background-image: url("https://www.honeywellaidc.com/-/media/en/images/solutions/connected-worker/overview/connected-workers-widescreen-banner-02.jpg?h=620&la=en-AU&w=1850&hash=BD43CE9D28882F16D8AAC7A179417C56F3E5E632");
  background-position: top;
  background-repeat: no-repeat;
  background-size: cover;
}
.slide-4 {
  background-image: url("https://www.trainer.ae/articles/wp-content/uploads/2015/05/personal-trainer.jpg");
  background-position: left;
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
<template>
  <div>
    <v-row>
      <v-col cols="12" order-sm="1" sm="5" md="2">
        <v-text-field
          :rules="[requiredRules]"
          v-model="cif"
          :outlined="true"
          label="Código Identificativo"
          hint="Introduzca su CIF, NIF, NIE..."
        ></v-text-field>
      </v-col>
      <v-col cols="12" order-sm="3" md="6">
        <v-text-field
          v-model="nameCompany"
          :rules="[requiredRules]"
          :outlined="true"
          label="Empresa"
          hint="Introduzca el nombre de su empresa"
        ></v-text-field>
      </v-col>
      <v-col cols="12" order-sm="2" sm="7" md="4">
        <v-text-field
          v-model="ActivityId"
          :rules="[requiredRules]"
          :outlined="true"
          label="Nombre actividad"
          hint="Introduzca el nombre de su empresa"
        ></v-text-field>
      </v-col>
      <v-col cols="12" order-sm="4">
        <v-text-field
          v-model="Address"
          :rules="[requiredRules]"
          :outlined="true"
          label="Dirección"
          hint="Introduzca la dirección"
        ></v-text-field>
      </v-col>
      <v-col cols="12" order-sm="5" sm="6" md="4">
        <v-text-field
          v-model="City"
          :rules="[requiredRules]"
          :outlined="true"
          label="Ciudad"
          hint="Introduzca la ciudad"
        ></v-text-field>
      </v-col>
      <v-col cols="12" order-sm="6" sm="6" md="4">
        <!-- https://datos.gob.es/apidata/nti/territory/Province?_sort=label&_pageSize=100 -->
        <v-text-field
          v-model="Province"
          :rules="[requiredRules]"
          :outlined="true"
          label="Provincia"
          hint="Introduzca la provincia"
        ></v-text-field>
      </v-col>
      <v-col cols="5" order-sm="7">
        <v-text-field
          v-model="Pc"
          :rules="[requiredRules]"
          :outlined="true"
          label="Codigo Postal"
          hint="Introduzca el código postal"
        ></v-text-field>
      </v-col>
      <v-col cols="7" order-sm="8">
        <v-text-field
          v-model="Country"
          :rules="[requiredRules]"
          :outlined="true"
          label="Pais"
          hint="Introduzca el pais"
        ></v-text-field>
      </v-col>

      <v-col cols="3" order-sm="9">
        <v-text-field
          v-model="Ext"
          :rules="[requiredRules]"
          :outlined="true"
          label="Codigo pais"
          hint="Introduzca el código del pais"
        ></v-text-field>
      </v-col>
      <v-col order-sm="10" cols="9">
        <v-text-field
          v-model="Phone"
          :rules="[requiredRules]"
          :outlined="true"
          label="Telefono"
          hint="Introduzca el teléfono de contacto"
        ></v-text-field>
      </v-col>
    </v-row>

    <v-text-field
      v-model="Email"
      :rules="[requiredRules]"
      :outlined="true"
      label="Email contacto"
      hint="Introduzca el correo electrónico de contacto"
    ></v-text-field>
    <v-text-field
      v-model="MaxDistance"
      :rules="[requiredRules]"
      :outlined="true"
      label="Distancia máxima"
      hint="Introduzca la distancia máxima a desplazarse"
    ></v-text-field>
    <v-text-field
      v-model="Description"
      :rules="[requiredRules]"
      :outlined="true"
      label="Descripción"
      hint="Introduzca una descripción de su empresa y los servicios que ofrece"
    ></v-text-field>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
@Component
export default class CreateCompany extends Vue {
  public isValid = false;
  public cif = "";
  public nameCompany = "";
  public Address = "";
 
  public City = "";
  public Province = "";
  public Pc = "";
  public Country = "";
  public Ext = "";
  public Phone = "";
  public Email = "";
  public ActivityId = 0;
  public MaxDistance = 50;
  public Description = "";
  public Availability = true;
  public requiredRules = (v: any) => !!v || "Required";
}
</script>
<style scoped>
</style>
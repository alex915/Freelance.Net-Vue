
  <template>
  <div class="padding">
    <div>
      <v-row class="fill-height">
        <v-col>
          <v-sheet height="64">
            <v-toolbar flat color="white">
              <v-btn
                outlined
                class="mr-4 display__none"
                color="blue darken-2"
                @click="setToday"
              >
                Hoy
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn fab text small color="grey darken-2" @click="prev">
                <v-icon small>mdi-chevron-left</v-icon>
              </v-btn>
              <v-toolbar-title v-if="$refs.calendar">
                {{ $refs.calendar.title }}
              </v-toolbar-title>
              <v-btn fab text small color="grey darken-2" @click="next">
                <v-icon small>mdi-chevron-right</v-icon>
              </v-btn>
              <v-spacer></v-spacer>
              <v-menu bottom right>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    outlined
                    color="blue darken-2"
                    v-bind="attrs"
                    v-on="on"
                  >
                    <span>{{ typeToLabel[type] }}</span>
                    <v-icon right>mdi-menu-down</v-icon>
                  </v-btn>
                </template>
                <v-list>
                  <v-list-item @click="type = 'day'">
                    <v-list-item-title>Dia</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = 'week'">
                    <v-list-item-title>Semana</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = 'month'">
                    <v-list-item-title>Mes</v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="type = '4day'">
                    <v-list-item-title>Últimos 4 dias</v-list-item-title>
                  </v-list-item>
                </v-list>
              </v-menu>
            </v-toolbar>
          </v-sheet>
          <v-sheet height="600">
            <v-calendar
              locale="es"
              ref="calendar"
              v-model="focus"
              color="light-blue darken-1 "
              :events="events"
              :event-color="getEventColor"
              :type="type"
              :short-weekdays="false"
              @click:event="showEvent"
              @click:more="viewDay"
              @click:date="viewDay"
            ></v-calendar>
            <v-menu
              v-model="selectedOpen"
              :close-on-content-click="false"
              :activator="selectedElement"
              offset-x
            >
              <v-card color="grey lighten-4" min-width="350px" flat>
                <v-toolbar :color="selectedEvent.color" dark>
                  <v-list-item-avatar>
                    <img
                      :src="'data:image/jpg;base64,' + selectedEvent.avatar"
                      srcset="..\assets\profile-user.png"
                    />
                  </v-list-item-avatar>
                  <v-toolbar-title
                    >{{ selectedEvent.name }}
                    {{ selectedEvent.lastName }}</v-toolbar-title
                  >
                  <v-spacer></v-spacer>
                </v-toolbar>
                <v-card-text>
                  <!-- <span v-html="selectedEvent.details"></span> -->
                  <a class="link" :append-icon="mdi-email" :href="'mailTo:' + selectedEvent.email">
                   <v-icon>mdi-email</v-icon>   {{ selectedEvent.email }}
                  </a>
                  <v-divider class="my-4"></v-divider>
                  <a class="link" :href="'tel:+' + selectedEvent.ext + selectedEvent.phone">
                   <v-icon>mdi-phone</v-icon>  +{{ selectedEvent.ext}} {{ selectedEvent.phone }}
                  </a>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn text color="secondary" @click="selectedOpen = false">
                    Cancelar
                  </v-btn>
                  <v-btn
                    v-if="!selectedEvent.contacted"
                    text
                    color="primary"
                    @click="contact(selectedEvent.id)"
                  >
                    Contactado
                  </v-btn>
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-menu>
          </v-sheet>
        </v-col>
      </v-row>
    </div>
  </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { contactsService } from "@/services/contacts.service";

@Component({
  components: {},
})
export default class Contacts extends Vue {
  public contacts: any[] = [];

  public focus = "";
  public type = "month";
  public typeToLabel = {
    month: "Mes",
    week: "Semana",
    day: "Dia",
    "4day": "Últimos 4 dias",
  };
  public selectedEvent: any = {};
  public selectedElement = null;
  public selectedOpen = false;
  public events: any[] = [];
  public colors = ["blue", "indigo", "cyan"];
  public categories: any = ["Pendientes de contactar", "Contactados"];

  public contact(id: any) {
    this.$spinner.showSpinner();
    contactsService
      .viewContact(id)
      .then(() => {
        this.selectedEvent.contacted = true;
        this.selectedEvent.color = this.colors[1];
        this.$refs.calendar.checkChange();
        this.selectedOpen = false;
      })
      .finally(this.$spinner.removeSpinner());
  }
  public mounted() {
    const user = this.$store.getters["user"];
    this.$spinner.showSpinner();
    contactsService
      .getContacts(user?.company?.id)
      .then((x) => {
        this.contacts = x.data.$values.map((contact: any) => {
          contact.date = new Date(contact.date);
          return contact;
        });
        this.updateRange();
        this.$refs.calendar.checkChange();
      })
      .finally(this.$spinner.removeSpinner());
  }
  public viewDay({ date }) {
    this.focus = date;
    this.type = "day";
  }
  public getEventColor(event: any) {
    return event.color;
  }
  public setToday() {
    this.focus = "";
  }
  public prev() {
    this.$refs.calendar.prev();
  }
  public next() {
    this.$refs.calendar.next();
  }
  public showEvent({ nativeEvent, event }) {
    const open = () => {
      this.selectedEvent = event;
      this.selectedElement = nativeEvent.target;
      setTimeout(() => (this.selectedOpen = true), 10);
    };

    if (this.selectedOpen) {
      this.selectedOpen = false;
      setTimeout(open, 10);
    } else {
      open();
    }

    nativeEvent.stopPropagation();
  }
  public updateRange() {
    const events: any[] = [];
    if (!this.contacts.length) {
      this.events = events;
      return;
    }
    this.contacts.forEach((contact: any) => {
      events.push({
        id: contact?.id,
        name: contact?.user?.firstName,
        lastName: contact?.user?.lastName,
        start: contact?.date,
        end: contact?.date,
        phone: contact?.user?.phone,
        ext: contact?.user?.ext,
        email: contact?.user?.email,
        contacted: contact?.contacted,
        color: this.colors[contact?.contacted ? 0 : 1],
        timed: true,
        category: this.categories[contact?.contacted ? 0 : 1],
      });
    });
    console.log(events);
    this.events = events;
  }
}
</script>

<style scoped>
.padding {
  padding: 10px;
}
.padding > div {
  background-color: white;
  box-shadow: 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 2px 2px 0 rgba(0, 0, 0, 0.14),
    0 1px 5px 0 rgba(0, 0, 0, 0.12);
  padding: 50px;
}
::v-deep .v-present .v-btn__content {
  color: white;
}
.link{
      color: grey;
    text-decoration: none;
}
.link:hover{
      color: #2f9fe9;
}
@media (max-width: 720px) {
  .padding > div {
    padding: 5px;
  }
  .display__none {
    display: none;
  }
}
</style>
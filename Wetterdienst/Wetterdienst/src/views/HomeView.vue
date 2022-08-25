<script setup lang="ts">
import BlaueBox from "../components/BlaueBox.vue"
import Ueberschrift from "../components/Ueberschrift.vue"
const weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
import { onMounted, ref } from 'vue'


const weather = ref({
  temperatur: 12,
  date: new Date(),
  stimmung: "icon",
  regen: "💧 10 mm",
})

onMounted(() => {
  fetch("https://localhost:7220/WeatherForecast/Today")
    .then(response => response.json())
    .then(data => {
      console.log({ data })
      weather.value = {
        date: new Date(data.date),
        temperatur: data.temperatureC,
        stimmung: data.icon,
        regen: "💧 " + data.regen + " mm"
      }
    })
})


let id = 0
const wochenArray = ref([
  { id: id++, datum: "15.08.2022", tag: 'Montag', stimmung: '🌞', temperatur: 30, regen: "💧 15%" },
  { id: id++, datum: "16.08.2022", tag: "Dienstag", stimmung: "🌞", temperatur: 18, regen: "💧 10%" },
  { id: id++, datum: "17.08.2022", tag: "Mittwoch", stimmung: "🌞", temperatur: 30, regen: "💧 20%" },
  { id: id++, datum: "18.08.2022", tag: "Donnerstag", stimmung: "🌞", temperatur: 35, regen: "💧 10%" },
  { id: id++, datum: "19.08.2022", tag: "Freitag", stimmung: "🌞", temperatur: 30, regen: "💧 70%" },
  { id: id++, datum: "20.08.2022", tag: "Samstag", stimmung: "🌞", temperatur: 28, regen: "💧 30%" },
  { id: id++, datum: "21.08.2022", tag: "Sonntag", stimmung: "🌞", temperatur: 20, regen: "💧 10%" }
])


onMounted(() => {
  fetch("https://localhost:7220/WeatherForecast")
    .then(response => {
      return response.json();
    })
    .then(data => {
      console.log({ data })
      wochenArray.value = data.map((entry: any) => {
        const timestamp = new Date(entry.date)
        return {
          id: id++,
          datum: timestamp.toLocaleTimeString(),
          tag: weekdays[timestamp.getDay()],
          stimmung: entry.icon,
          temperatur: entry.temperatureC,
          regen: "💧 " + entry.regen + " mm"
        }
      })
    }
    )
})

</script>

<template>

  <div>
    <div class="startcontainer">
      <!-- <div Title>
        <h1 style="color: rgb(0,0,0)">
          <b> 🌞🌤️🌧️ Der nicht ganz so sonnige Wetterdienst! 🌞🌤️🌧️ </b>
        </h1>

      </div> -->
      <div class="firstelement">
        <Ueberschrift>
          <h2>
            <u>Heute:</u>
            <br /> {{ ' ' + weather.date.toLocaleDateString() }}
            <br />
            🌞 {{ Math.round(weather.temperatur * 10) / 10 }}°
            <br />
            {{ weather.regen }}
            <br />
            📍 Koblenz
          </h2>
        </Ueberschrift>
      </div>
      <div class="secondelement">
        <BlaueBox>
          <table>
            <tr>
              <th>Wochentag</th>
              <th>Datum</th>
            </tr>
            <tr v-for="tag in wochenArray" :key="tag.id">
              <th>{{ tag.tag }}</th>
              <th>{{ tag.datum }}</th>
              <td><img :src="'http://openweathermap.org/img/w/' + tag.stimmung + '.png'" /></td>
              <td>{{ Math.round(tag.temperatur * 10) / 10 }} ° </td>
              <td>{{ tag.regen }}</td>
            </tr>
          </table>
        </BlaueBox>
      </div>

    </div>

  </div>

</template>

<style scoped>
.menuecontainer {
  font-size: 1.25rem;
  /* border: 3px solid rgb(0, 0, 0); */
  background-color: rgb(235, 231, 231);
  border-radius: 10px;
  padding-top: 5px;
  padding-right: 15px;
  padding-bottom: 5px;
  padding-left: 10px;
}

.firstelement {
  flex: 1;
  display: flex;
  align-items: stretch;
}

.secondelement {
  flex: 3;
  display: flex;
  align-items: stretch;
}

.startcontainer {
  display: flex;
  justify-content: space-between;
  flex-direction: column;
  align-items: stretch;
  text-align: center;
  flex: 1;
  gap: 10px;
  /* Margin same height as Header */
  margin-top: 86px;
}

@media (min-width: 1000px) {
  .startcontainer {
    flex-direction: row;
  }
}

/* .titlecontainer {
  background-color: rgb(26, 36, 44);
  border-radius: 10px;
  padding-top: 5px;
  padding-right: 10px;
  padding-bottom: 10px;
  padding-left: 10px;
} */
</style>
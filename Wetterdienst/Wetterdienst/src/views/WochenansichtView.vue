<script setup lang="ts">
import Ueberschrift from '../components/Ueberschrift.vue'
import BlaueBox from "../components/BlaueBox.vue"
import { onMounted, ref } from 'vue'

const weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

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
            <Ueberschrift>
                <H2>Wochenansicht</H2>
            </Ueberschrift>
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
</template>
<style>
</style>

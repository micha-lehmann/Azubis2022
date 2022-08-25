<script setup lang="ts">
import Title from "./components/Title.vue";
</script>

<template>
  <div id="app">
    <Title></Title>
    <RouterView />
  </div>
</template>

<style >
table {
  font-size: 1.25rem;
  text-align: left;
}
</style>

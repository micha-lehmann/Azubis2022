<script setup lang="ts"></script>

<template>
  <div class="ueberschrift">
    <slot></slot>
  </div>
</template>

<style scoped>
.ueberschrift {
  font-size: 1.1rem;
  background-color: rgb(235, 231, 231);
  flex: 1;
  border-radius: 10px;
  padding-top: 5px;
  padding-right: 15px;
  padding-bottom: 5px;
  padding-left: 10px;
}

@media (min-width: 1000px) {
  .ueberschrift {
    font-size: 24px;
  }
}
</style>
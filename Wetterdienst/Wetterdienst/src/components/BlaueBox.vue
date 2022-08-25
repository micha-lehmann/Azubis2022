<script setup lang="ts"></script>

<template>
    <div class="wetterheutecontainer">
        <slot></slot>
    </div>
</template>

<style scoped>
.wetterheutecontainer {
    background-color: rgb(248, 112, 0);
    border: 4px solid black;
    border-radius: 10px;
    padding-top: 5px;
    padding-right: 10px;
    padding-bottom: 10px;
    padding-left: 10px;
    display: flex;
    flex-direction: column;
    align-items: stretch;
    flex: 1
}
</style>
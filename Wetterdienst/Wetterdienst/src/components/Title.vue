<script setup lang="ts"></script>

<template>
    <div class="title">
        <h1 style="color: rgb(0,0,0)">
            🌞🌤️ Wetterdienst! 🌞🌤️🌧️
        </h1>
    </div>
</template>

<style scoped>
.title {
    background-color: rgb(255, 255, 255);
    margin-bottom: 50px;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 86px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.title h1 {
    font-size: 18px;
}

@media (min-width: 1000px) {
    .title h1 {
        font-size: 32px;
    }
}
</style>

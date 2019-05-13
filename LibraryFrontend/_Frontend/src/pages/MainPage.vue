<template>
  <div>
    <h1 class="display-4">LENDING</h1>
    <lending-component></lending-component>
  </div>

</template>

<script>
    import LendingComponent from "../components/LendingComponent";
    export default {
        name: "MainPage",
      components: {
          "lending-component": LendingComponent
        }
    }
</script>

<style scoped>

</style>

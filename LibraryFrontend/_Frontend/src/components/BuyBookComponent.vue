<template>
  <div>
    <h1>BUY BOOK</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Book:</h4>
        </b-col>
        <b-col>
          <b-dropdown></b-dropdown>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Address:</h4>
        </b-col>
        <b-col>
          <input>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary">Add Supplier</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
    export default {
        name: "BuyBookComponent"
    }
</script>

<style scoped>

</style>

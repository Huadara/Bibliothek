<template>
  <div>
    <h1>CUSTOMER</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Firstname:</h4>
        </b-col>
        <b-col>
          <input>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Lastname:</h4>
        </b-col>
        <b-col>
          <input>
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
          <h4>Credit Nr:</h4>
        </b-col>
        <b-col>
          <input>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary">Send Customer ID</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
    export default {
        name: "AddCustomerComponent"
    }
</script>

<style scoped>

</style>

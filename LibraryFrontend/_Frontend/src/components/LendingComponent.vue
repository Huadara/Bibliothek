<template>
  <b-container>
    <b-row>
        <b-col>
          <div>
            <h3>Customer</h3>
            <input class="form-control" placeholder="Search Customer ID">
          </div>
        </b-col>
        <b-col>

            <b-dropdown  id="whitespace">
              <b-dropdown-item>gay</b-dropdown-item>
            </b-dropdown>
        </b-col>
    </b-row>
    <b-row>
        <b-col>
          <b-button variant="primary">Send Customer ID</b-button>
        </b-col>
    </b-row>
  </b-container>
</template>

<script>
    export default {
        name: "LendingComponent"
    }
</script>

<style scoped>
  #whitespace{
      padding-top: 2.6em;
  }
</style>

<template>
  <div class="container slim_scrollbar">
    <div class="header_wrap">
      <slot name="header">
        <template>
          <mu-appbar :title="title" color="pink">
            <mu-button icon slot="left" v-if="has_menu && layout_type" @click="openMenu" >
              <mu-icon value="menu"></mu-icon>
            </mu-button>
            <mu-button icon slot="left"  @click="goBack" >
              <mu-icon value="arrow_back"></mu-icon>
            </mu-button>
            <!-- <mu-menu icon="more_vert" slot="right" v-if="has_share">
                <mu-button icon>
                  <mu-icon value="more_vert"></mu-icon>
                </mu-button>
              <slot name="bar_menu">
                <mu-menu-item title="分享" @click="share" />
                <mu-menu-item to="/user/favorite" title="我的收藏" />
              </slot>
            </mu-menu> -->
            <mu-button flat slot="right" v-if="$store.state.user.is_login"  @click="loginOut()" >
              退出
            </mu-button>
          </mu-appbar>
        </template>
      </slot>
    </div>
    <div class="side" v-if="has_menu && layout_type">
      <mu-drawer :docked="docked" :open="open" @close="openMenu()">
        <div class="img_box" :style="`background-image: url(${bg})`">
        </div>
        <mu-list>
          <mu-list-item :title="nav.title" :to="nav.value" v-for="nav in bottom_nav" :key="nav.title" @click="openMenu()">
            <mu-icon slot="left" :value="nav.icon" />
          </mu-list-item>
        </mu-list>
      </mu-drawer>
    </div>
    <div class="content_wrap">
      <slot></slot>
    </div>
    <mu-dialog :open="dialog" title="分享到" @close="closeDialog">
      <div id="soshid"></div>
      <mu-flat-button slot="actions" @click="closeDialog" primary label="关闭" />
    </mu-dialog>
  </div>
</template>
<script>
let _self;
import Layout from "@/components/Layout";
import Store from "storejs";
import bg from "../assets/images/bg.png";

export default {
  data: function() {
    return {
      bg,

      open: false,
      docked: false,
      layout_type: false,
      dialog: false
    };
  },
  props: {
    title: {
      type: String,
      default: ""
    },
    has_menu: {
      type: Boolean,
      default: true
    },
    has_share: {
      type: Boolean,
      default: true
    },
    leftAction: {
      type: Function
    },
    has_footer: {
      type: Boolean,
      default: true
    }
  },
  created() {
    _self = this;
  },
  mounted() {
    // 设置手机状态栏颜色
    // this.setStatusBar();
  },
  activated() {
    this.has_footer
      ? this.$store.dispatch("showNav")
      : this.$store.dispatch("hideNav");
    this.setActiveNav();
    this.getModSwitch();
    this.setStatusBar();
  },
  methods: {
    openMenu() {
      this.open = !this.open;
    },
    search() {
      this.$router.push("/");
    },
    goBack() {
      if (this.leftAction) {
        this.leftAction.call(this.$parent);
      } else {
        if (history.length > 1) {
          this.$router.go(-1);
        } else {
          this.$router.push("/");
        }
      }
    },
    loginOut() {
      this.$store.dispatch("loginOut");
      // this.$router.replace("/");
    },
    setActiveNav() {
      let path = this.$route.path;
      this.active_nav = path;
    },
    setStatusBar() {
      let theme = Store.get("theme") || "";
      let theme_color = "#7E57C2";
      if (theme === "def") {
        theme_color = "#7E57C2";
      }
      if (theme === "light") {
        theme_color = "#2196f3";
      }
      if (theme === "dark") {
        theme_color === "#424242";
      }
      if (theme === "carbon") {
        theme_color = "#474a4f";
      }
      if (theme === "teal") {
        theme_color = "#009688";
      }
      document.addEventListener(
        "deviceready",
        () => {
          console.log("设备已就绪");
          StatusBar.backgroundColorByHexString("#e91e63");
        },
        false
      );
    },
    getModSwitch() {
      let a = Store.get("mod_switch") || false;
      this.layout_type = a;
    },
    share() {
      if (this.isCordova) {
        this.initCordovaShare();
      } else {
        this.dialog = true;
        this.initWebShare();
      }
    },
    initCordovaShare() {
      let opt = {};
      opt.url = location.href;
      opt.message = "观影报名_哈迷有求必应屋";
      window.plugins.socialsharing.shareWithOptions(
        opt,
        onSuccess => {
          console.log(onSuccess);
        },
        onError => {
          console.log(onError);
        }
      );
    },
    initWebShare() {
      let opt = {};
      opt.url = location.href;
      opt.title = "观影报名_哈迷有求必应屋";
      opt.digest = "";
      opt.sites = ["weixin,", "weibo", "qzone", "tqq", "douban", "tieba"];

      this.$nextTick(() => {
        sosh("#soshid", opt);
      });
    },
    closeDialog() {
      this.dialog = false;
    }
  },
  computed: {
    isCordova() {
      return this.$store.state.user.is_cordova;
    }
  },
  components: {}
};
</script>
<style scoped lang="less">
.container {
  width: 100%;
  height: 100%;
  padding: 0 0;
  overflow-y: auto;
  .header_wrap {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    width: 100%;
    z-index: 999;
  }
  .side {
    .img_box {
      height: 170px;
      background-repeat: no-repeat;
      background-size: cover;
    }
  }
  .content_wrap {
    padding: 56px 0 56px 0; // background: #f5f5f5;
  }
  .footer_wrap {
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    width: 100%;
  }
}
</style>

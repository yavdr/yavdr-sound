<?cs if:(module.sound == "alsa" && sound.alsa.output != "") ?><?cs if:(sound.alsa.output == "hdmi" || sound.alsa.output == "passthrough") ?>pcm.!default {
  type hw
  card <?cs var:sound.alsa.hdmi.card ?>
  device <?cs var:sound.alsa.hdmi.device ?>
}<?cs elif:sound.alsa.output == "spdif" ?>pcm.!default {
  type hw
  card <?cs var:sound.alsa.spdif.card ?>
  device <?cs var:sound.alsa.spdif.device ?>
}<?cs elif:sound.alsa.output == "all" ?>pcm.!default {
  type plug
  slave {
    pcm "both"
  }
}
pcm.both {
  type route
  slave {
    pcm multi
    channels 4
  }
  ttable.0.0 1.0
  ttable.1.1 1.0
  ttable.0.2 1.0
  ttable.1.3 1.0
}
pcm.multi {
  type multi
  slaves.a {
    pcm "hdmi"
    channels 2
  }
  slaves.b {
    pcm "spdif"
    channels 2
  }
  bindings.0.slave a
  bindings.0.channel 0
  bindings.1.slave a
  bindings.1.channel 1
  bindings.2.slave b
  bindings.2.channel 0
  bindings.3.slave b
  bindings.3.channel 1
}
pcm.hdmi {
  type dmix
  ipc_key 1024
  slave {
    pcm "hw:<?cs var:sound.alsa.hdmi.card ?>,<?cs var:sound.alsa.hdmi.device ?>" period_time 0
    period_size 1024
    buffer_size 4096
    rate 48000
    channels 2
  }
}
pcm.spdif {
  type dmix
  ipc_key 1025
  slave {
    pcm "hw:<?cs var:sound.alsa.spdif.card ?>,<?cs var:sound.alsa.spdif.device ?>" period_time 0
    period_size 1024
    buffer_size 4096
    rate 48000
    channels 2
  }
}<?cs /if ?><?cs /if ?>

<template>
    <div class="wrapper">
        <div class="loading">
            <span class="loader"></span>
            <span class="loader"></span>
            <span class="loader"></span>
        </div>
    </div>
</template>


<script>
export default {
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped>
* {
    margin: 0;
    padding: 0;
    list-style: none;
    border: 0;
    outline: 0;
    -webkit-tap-highlight-color: transparent;
    text-decoration: none;
    color: inherit;
    box-sizing: border-box;

    &:focus {
        outline: 0;
    }
}

.about {
    $cubic: cubic-bezier(0.64, 0.01, 0.07, 1.65);
    $transition: 0.6s $cubic;
    $size: 40px;
    position: fixed;
    z-index: 10;
    bottom: 10px;
    right: 10px;
    width: $size;
    height: $size;
    display: flex;
    justify-content: flex-end;
    align-items: flex-end;
    transition: all 0.2s ease;

    .bg_links {
        width: $size;
        height: $size;
        border-radius: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        background-color: rgba(#000000, 0.2);
        border-radius: 100%;
        backdrop-filter: blur(5px);
        position: absolute;
    }

    .logo {
        width: $size;
        height: $size;
        z-index: 9;
        background-image: url(https://rafaelavlucas.github.io/assets/codepen/logo_white.svg);
        background-size: 50%;
        background-repeat: no-repeat;
        background-position: 10px 7px;
        opacity: 0.9;
        transition: all 1s 0.2s ease;
        bottom: 0;
        right: 0;
    }

    .social {
        opacity: 0;
        right: 0;
        bottom: 0;

        .icon {
            width: 100%;
            height: 100%;
            background-size: 20px;
            background-repeat: no-repeat;
            background-position: center;
            background-color: transparent;
            display: flex;
            transition: all 0.2s ease, background-color 0.4s ease;
            opacity: 0;
            border-radius: 100%;
        }

        &.portfolio {
            transition: all 0.8s ease;

            .icon {
                background-image: url(https://rafaelavlucas.github.io/assets/codepen/link.svg);
            }
        }

        &.dribbble {
            transition: all 0.3s ease;

            .icon {
                background-image: url(https://rafaelavlucas.github.io/assets/codepen/dribbble.svg);
            }
        }

        &.linkedin {
            transition: all 0.8s ease;

            .icon {
                background-image: url(https://rafaelavlucas.github.io/assets/codepen/linkedin.svg);
            }
        }
    }

    &:hover {
        width: 105px;
        height: 105px;
        transition: all $transition;

        .logo {
            opacity: 1;
            transition: all 0.6s ease;
        }

        .social {
            opacity: 1;

            .icon {
                opacity: 0.9;
            }

            &:hover {
                background-size: 28px;

                .icon {
                    background-size: 65%;
                    opacity: 1;
                }
            }

            &.portfolio {
                right: 0;
                bottom: calc(100% - 40px);
                transition: all 0.3s 0s $cubic;

                .icon {
                    &:hover {
                        background-color: #698fb7;
                    }
                }
            }

            &.dribbble {
                bottom: 45%;
                right: 45%;
                transition: all 0.3s 0.15s $cubic;

                .icon {
                    &:hover {
                        background-color: #ea4c89;
                    }
                }
            }

            &.linkedin {
                bottom: 0;
                right: calc(100% - 40px);
                transition: all 0.3s 0.25s $cubic;

                .icon {
                    &:hover {
                        background-color: #0077b5;
                    }
                }
            }
        }
    }
}


body {
    background-color: #1f2029;
}

.wrapper {
    width: 100%;
    height: 60vh;
    display: flex;
    justify-content: center;
    align-items: center;


    .loading {
        display: flex;
        position: absolute;
        width: auto;
        height: 20px;
        transition: all 0.4s ease;

        .loader {
            $loader: loader 1.2s ease infinite both;
            display: block;
            align-items: flex-end;
            background-color: #ff6f24;
            width: 5px;
            height: 5px;
            position: absolute;
            animation: $loader;
            border-radius: 4px;
            bottom: 0;

            &:nth-child(1) {
                transform: translateX(-20px);
            }

            &:nth-child(2) {
                animation-delay: 0.4s;
            }

            &:nth-child(3) {
                animation-delay: 0.8s;
                transform: translateX(20px);
            }

            @keyframes loader {
                0% {
                    height: 5px;
                    bottom: 0;
                    opacity: 1;
                }

                25% {
                    opacity: 0.3;
                    bottom: 0;
                    height: 15px;
                }

                50% {
                    opacity: 1;
                    height: 5px;
                    bottom: calc(100% - 5px);
                }

                75% {
                    opacity: 0.3;
                    height: 15px;
                    bottom: 0;
                }

                100% {
                    opacity: 1;
                    height: 5px;
                    bottom: 0;
                }
            }
        }
    }
}
</style>
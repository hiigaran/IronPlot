#lang racket/base
(letrec ([f (λ (x) (g x))]
               [g (λ (y) y)])
        (f 10))
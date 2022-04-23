.data

; data for ProcSrednia
first dq 0
sec dq 0
dest dq 0

.code

Dodaj proc

push rbx
mov rbx, rcx
mov rbx, [rbx]
movups XMM0, [rbx]
mov rbx, [rcx+8]
movups XMM1, [rbx]
addps xmm0, xmm1
mov rbx, [rcx+16]
movups [rbx], xmm0
pop rbx
ret
Dodaj endp



Odejmij proc
push rbx
mov rbx, rcx
mov rbx, [rbx]
movups XMM0, [rbx]
mov rbx, [rcx+8]
movups XMM1, [rbx]
subps xmm0, xmm1
mov rbx, [rcx+16]
movups [rbx], xmm0
pop rbx
ret
Odejmij endp


Pomnoz proc
push rbx
mov rbx, rcx
mov rbx, [rbx]
movups XMM0, [rbx]
mov rbx, [rcx+8]
movups XMM1, [rbx]
mulps xmm0, xmm1
mov rbx, [rcx+16]
movups [rbx], xmm0
pop rbx
ret
Pomnoz endp


Podziel proc
push rbx
mov rbx, rcx
mov rbx, [rbx]
movups XMM0, [rbx]
mov rbx, [rcx+8]
movups XMM1, [rbx]
divps xmm0, xmm1
mov rbx, [rcx+16]
movups [rbx], xmm0
pop rbx
ret
Podziel endp

END
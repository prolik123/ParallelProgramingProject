import matplotlib.pyplot as plt

plt.xlabel('Number of threads')
plt.ylabel('Time [s]')
plt.title('Performace [|V| = 1e7, |E| = 1e7]')
plt.plot([1, 2, 4, 8, 16], [85, 85, 85, 85, 85], color='red')
plt.plot([1, 2, 4, 8, 16], [220, 120, 90, 75, 78], color='blue')
plt.plot([1, 2, 4, 8, 16], [185, 108, 82, 62, 63], color='green')
plt.plot([1, 2, 4, 8, 16], [142, 78, 57, 51, 46], color='orange')
plt.legend(['Single thread', 'Threads', 'Thread Pool', 'Lock data structures + Thread Pool'])
plt.show()